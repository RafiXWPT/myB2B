using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Domain.EntityFramework;
using MyB2B.Server.Documents.Generators;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Commands.Decorators;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.Actions.Queries.Decorators;
using MyB2B.Web.Infrastructure.Authorization;
using MyB2B.Web.Infrastructure.Authorization.UserService;
using MyB2B.Web.Infrastructure.Dependency;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace MyB2B.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private Container Container { get; } = DependencyContainer.Container;
        private Assembly[] ApplicationAssemblies { get; } = { typeof(Program).Assembly, typeof(Infrastructure.Actions.ActionResult<>).Assembly };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            IntegrateSimpleInjector(services);
            RegisterDependencies();

            var serverSecurityTokenSecret = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Security:Token:Secret"));
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.Events = new JwtBearerEvents
                {
                    OnTokenValidated = OnTokenValidatedAction
                };
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(serverSecurityTokenSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
               

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
        }

        private async Task OnTokenValidatedAction(TokenValidatedContext context)
        {
            var userService = Container.GetInstance<IUserService>();
            var applicationPrincipal = new ApplicationPrincipal(context.Principal);

            try
            {
                applicationPrincipal.ValidateUserEndpoint(context.SecurityToken as JwtSecurityToken, context.HttpContext.Request.Host.Value);
            }
            catch (UserEndpointMismatchException)
            {
                context.Fail("Mismatch between endpoint adressess.");
            }

            var user = userService.GetById(applicationPrincipal.UserId);
            if (user.IsFail)
                context.Fail("Not existing");

            await Task.CompletedTask;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);
            Container.Verify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            if(!Configuration.GetValue<bool>("EntityFrameworkConfiguration:InMemoryDatabase")) {
                using (AsyncScopedLifestyle.BeginScope(Container))
                {
                    Container.GetService<MyB2BContext>().Database.Migrate();
                }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            Container.RegisterInstance<IServiceProvider>(Container);
            Container.RegisterInstance<IConfiguration>(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(Container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(Container));

            services.EnableSimpleInjectorCrossWiring(Container);
            services.UseSimpleInjectorAspNetRequestScoping(Container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            Container.RegisterMvcControllers(app);
            Container.RegisterMvcViewComponents(app);
            Container.AutoCrossWireAspNetComponents(app);
        }

        private void RegisterDependencies()
        {
            RegisterDbContext();
            RegisterQueryHandlers();
            RegisterCommandHandlers();
            RegisterServices();
        }

        private void RegisterDbContext()
        {
            if(Configuration.GetValue<bool>("EntityFrameworkConfiguration:InMemoryDatabase")) {
                Container.RegisterInstance(new DbContextOptionsBuilder<MyB2BContext>().UseInMemoryDatabase().Options);
            } else {
                Container.RegisterInstance(new DbContextOptionsBuilder<MyB2BContext>().UseSqlServer(Configuration.GetValue<string>("EntityFrameworkConfiguration:ConnectionString")).Options);
            }
            Container.Register<MyB2BContext>(Lifestyle.Scoped);
            Container.RegisterInstance<Func<MyB2BContext>>(Container.GetInstance<MyB2BContext>);
        }

        private void RegisterQueryHandlers()
        {
            Container.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Singleton);

            Container.Register(typeof(IQueryHandler<,>), ApplicationAssemblies);
            Container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryHandlerProfilerDecorator<,>));
            Container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryHandlerLogDecorator<,>));
            Container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryHandlerExceptionDecorator<,>));

            Container.Register(typeof(IAsyncQueryHandler<,>), ApplicationAssemblies);
            Container.RegisterDecorator(typeof(IAsyncQueryHandler<,>), typeof(AsyncQueryHandlerProfilerDecorator<,>));
            Container.RegisterDecorator(typeof(IAsyncQueryHandler<,>), typeof(AsyncQueryHandlerLogDecorator<,>));
            Container.RegisterDecorator(typeof(IAsyncQueryHandler<,>), typeof(AsyncQueryHandlerExceptionDecorator<,>));
        }

        private void RegisterCommandHandlers()
        {
            Container.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Singleton);

            Container.Register(typeof(ICommandHandler<>), ApplicationAssemblies);
            Container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandHandlerProfileDecorator<>));
            Container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandHandlerLogDecorator<>));
            Container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandHandlerExceptionDecorator<>));

            Container.Register(typeof(IAsyncCommandHandler<>), ApplicationAssemblies);
            Container.RegisterDecorator(typeof(IAsyncCommandHandler<>), typeof(AsyncCommandHandlerProfileDecorator<>));
            Container.RegisterDecorator(typeof(IAsyncCommandHandler<>), typeof(AsyncCommandHandlerLogDecorator<>));
            Container.RegisterDecorator(typeof(IAsyncCommandHandler<>), typeof(AsyncCommandHandlerExceptionDecorator<>));
        }

        private void RegisterServices()
        {
            RegisterSingletons();
            RegisterScoped();
        }

        private void RegisterSingletons()
        {
            Container.Register<IUserService, UserService>(Lifestyle.Singleton);
        }

        private void RegisterScoped()
        {
            Container.Register<IInvoiceGenerator, PdfInvoiceGenerator>();
        }
    }
}
