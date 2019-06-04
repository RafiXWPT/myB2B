using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using MyB2B.Domain;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using MyB2B.Web.Infrastructure.Dependency;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MyB2B.Tests
{
    public class InMemoryPrincipal : IApplicationPrincipal
    {
        public int UserId => 0;

        public string FirstName => "Application";

        public string LastName => "Principal";

        public bool IsConfirmed => true;
    }

    public abstract class ComponentTest<TComponent>
        where TComponent : class
    {
        private Container Container { get; } = new Container();
        protected TComponent Component => Container.GetInstance<TComponent>();

        protected MyB2BContext DbContext => Container.GetInstance<MyB2BContext>();

        protected ComponentTest() : base()
        {
            Init();
            AdditionalRegistration();
        }

        protected void RegisterService<TInterface, TImplementation>()
            where TInterface: class
            where TImplementation : class, TInterface
        {
            Container.Register<TInterface, TImplementation>();
            Container.Verify();
        }

        protected void SetupContextData<TApplicationEntity>(params TApplicationEntity[] entitiesToSetUp)
            where TApplicationEntity : ApplicationEntity
        {
            var context = DbContext;
            context.AddRange(entitiesToSetUp);
            context.SaveChanges();
        }

        protected abstract Assembly[] TestAssemblies { get; }
        protected virtual void AdditionalRegistration() { }

        private void Init()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            Container.RegisterInstance<IServiceProvider>(Container);
            Container.RegisterInstance<IConfiguration>(configuration);

            Container.Register<TComponent>();
            Container.RegisterInstance<Func<IApplicationPrincipal>>(() => new InMemoryPrincipal());
            Container.RegisterInstance(new DbContextOptionsBuilder<MyB2BContext>().UseInMemoryDatabase("MyB2B").Options);
            Container.Register<MyB2BContext>(Lifestyle.Scoped);
            Container.RegisterInstance<Func<MyB2BContext>>(Container.GetInstance<MyB2BContext>);

            Container.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Singleton);
            Container.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Singleton);

            Container.Register(typeof(IQueryHandler<,>), TestAssemblies);
            Container.Register(typeof(IAsyncQueryHandler<,>), TestAssemblies);
            Container.Register(typeof(ICommandHandler<>), TestAssemblies);
            Container.Register(typeof(IAsyncCommandHandler<>), TestAssemblies);
        }

        protected void BeginTest(Action testAction)
        {
            using(AsyncScopedLifestyle.BeginScope(Container))
            {
                testAction();
                DbContext.Database.EnsureDeleted();
            }
        }
    }
}
