using Moq;
using MyB2B.Domain;
using MyB2B.Tests;
using MyB2B.Web.Controllers.Logic.Authentication;
using NUnit.Framework;
using System;
using System.Linq;

namespace MyB2B.Web.Controllers.Logic.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationLogicTests : ComponentTest<AuthenticationControllerLogic>
    {
        public AuthenticationLogicTests() : base() { }

        [Test, Order(1)]
        public void Test1()
        {
            Component.Authenticate("mleko", "cipa", "mleko");
            Context.Addresses.Add(new Address() { City = "Muszyna" });
            Context.SaveChanges();

            var muszyna = Context.Addresses.First(a => a.City == "Muszyna");
        }

        [Test, Order(2)]
        public void Test2()
        {
            var muszyna = Context.Addresses.First(a => a.City == "Muszyna");
            var x = 0;
        }

        [Test]
        public void Test3()
        {
            var x = 0;
        }
    }
}
