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
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
using MyB2B.Server.Documents.Generators;
using MyB2B.Web.Controllers.Logic;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Commands.Decorators;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.Actions.Queries.Decorators;
using MyB2B.Web.Infrastructure.ApplicationUsers;
using MyB2B.Web.Infrastructure.Dependency;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Hangfire;
using System.Linq;
using MyB2B.Web.Controllers.Logic.Authentication;

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
        private Assembly[] ApplicationAssemblies { get; } = { typeof(Program).Assembly, typeof(DependencyContainer).Assembly, typeof(ControllerLogic).Assembly };

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            IntegrateSimpleInjector(services);
            RegisterDependencies();

            if (!Configuration.GetValue<bool>("EntityFrameworkConfiguration:InMemoryDatabase"))
            {
                services.AddHangfire(options =>
                {
                    options.UseSqlServerStorage(Configuration.GetValue<string>("EntityFrameworkConfiguration:ConnectionString"));
                });
            }
                
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
            var dbContext = Container.GetInstance<MyB2BContext>();
            var applicationPrincipal = new ApplicationPrincipal(context.Principal);

            try
            {
                applicationPrincipal.ValidateUserEndpoint(context.SecurityToken as JwtSecurityToken, context.HttpContext.Request.Host.Value);
            }
            catch (UserEndpointMismatchException)
            {
                context.Fail("Mismatch between endpoint adressess.");
            }

            var user = dbContext.Users.Find(applicationPrincipal.UserId);
            if (user == null)
                context.Fail("Not existing");

            await Task.CompletedTask;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);
            Container.Verify();

            if (!Configuration.GetValue<bool>("EntityFrameworkConfiguration:InMemoryDatabase"))
            {
                app.UseHangfireDashboard();
                app.UseHangfireServer();
            }

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

#if DEBUG
            using(AsyncScopedLifestyle.BeginScope(Container))
            {
                var authenticationLogic = Container.GetInstance<AuthenticationLogic>();
                authenticationLogic.Register("rafal.palej", "rafal.palej@rpalej.pl", "rafal.palej", "rafal.palej", "127.0.0.1");
            }
#endif
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSimpleInjector(Container, options => options
                .AddAspNetCore()
                .AddControllerActivation()
                .AddViewComponentActivation()
                .AddPageModelActivation()
                .AddTagHelperActivation());

            Container.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Container.RegisterInstance<IServiceProvider>(Container);
            Container.RegisterInstance(Configuration);

            services.EnableSimpleInjectorCrossWiring(Container);
            services.UseSimpleInjectorAspNetRequestScoping(Container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            Container.AutoCrossWireAspNetComponents(app);
        }

        private void RegisterDependencies()
        {
            RegisterDbContext();
            RegisterQueryHandlers();
            RegisterCommandHandlers();
            RegisterServices();
            RegisterControllersLogic();
        }

        private void RegisterDbContext()
        {
            Container.RegisterInstance<Func<IApplicationPrincipal>>(() =>  Container.GetInstance<IHttpContextAccessor>().HttpContext.ExtractApplicationPrincipalInterface());
            if (Configuration.GetValue<bool>("EntityFrameworkConfiguration:InMemoryDatabase")) {
                Container.RegisterInstance(new DbContextOptionsBuilder<MyB2BContext>().UseInMemoryDatabase("MyB2B").Options);
            } else {
                Container.RegisterInstance(new DbContextOptionsBuilder<MyB2BContext>()
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetValue<string>("EntityFrameworkConfiguration:ConnectionString")).Options);
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
            RegisterScoped();
        }

        private void RegisterScoped()
        {
            Container.Register<IInvoiceGenerator, PdfInvoiceGenerator>();
        }

        private void RegisterControllersLogic()
        {
            var logicAssembly = typeof(IControllerLogic).Assembly;
            var logicImplementations = logicAssembly.GetExportedTypes().Where(t => typeof(IControllerLogic).IsAssignableFrom(t) && !t.IsAbstract).ToList();
            foreach (var logicImplementation in logicImplementations)
            {
                Container.Register(logicImplementation);
            }
        }
    }
}
