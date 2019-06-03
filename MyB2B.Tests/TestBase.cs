using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MyB2B.Domain.EntityFramework;
using MyB2B.Domain.Identity;
using MyB2B.Web.Infrastructure.Actions.Commands;
using MyB2B.Web.Infrastructure.Actions.Queries;
using SimpleInjector;
using System;
using System.Collections.Generic;
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

    public class InMemoryConfiguration : IConfiguration
    {
        public string this[string key] { get => ""; set { } }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return new List<IConfigurationSection>();
        }

        public IChangeToken GetReloadToken()
        {
            return null;
        }

        public IConfigurationSection GetSection(string key)
        {
            return null;
        }
    }

    public abstract class TestBase
    {
        protected MyB2BContext Context { get; }
        protected Container Container { get; }

        protected virtual void Init() { }

        protected TestBase()
        {
            Container = new Container();
            Init();
        }
    }

    public abstract class ComponentTest<TComponent> : TestBase
        where TComponent : class
    {
        protected TComponent Component => Container.GetInstance<TComponent>();

        protected override void Init()
        {
            Container.RegisterInstance<IServiceProvider>(Container);
            Container.RegisterInstance<IConfiguration>(new InMemoryConfiguration());

            Container.Register<TComponent>();
            Container.RegisterInstance<Func<IApplicationPrincipal>>(() => new InMemoryPrincipal());
            Container.RegisterInstance(new DbContextOptionsBuilder<MyB2BContext>().UseInMemoryDatabase("MyB2B").Options);
            Container.Register<MyB2BContext>();
            Container.RegisterInstance<Func<MyB2BContext>>(Container.GetInstance<MyB2BContext>);

            Container.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Singleton);
            Container.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Singleton);

            base.Init();
        }
    }
}
