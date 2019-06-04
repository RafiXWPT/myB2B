using MyB2B.Tests;
using MyB2B.Web.Infrastructure.Dependency;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyB2B.Web.Controllers.Logic.Tests
{
    public abstract class LogicTest<TLogic> : ComponentTest<TLogic> where TLogic : class, IControllerLogic
    {
        protected override Assembly[] TestAssemblies => new[] { typeof(DependencyContainer).Assembly, typeof(TLogic).Assembly };
    }
}
