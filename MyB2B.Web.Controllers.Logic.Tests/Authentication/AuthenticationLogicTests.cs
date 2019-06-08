using MyB2B.Domain.Identity;
using MyB2B.Web.Controllers.Logic.Authentication;
using NUnit.Framework;

namespace MyB2B.Web.Controllers.Logic.Tests.Authentication
{
    [TestFixture]
    public class AuthenticationLogicTests : LogicTest<AuthenticationLogic>
    {
        [Test, Order(1)]
        public void should_not_authenticate_if_no_user_with_that_username()
        {
            BeginTest(() =>
            {
                var authenticationResult = Component.Authenticate("test_user", "test_password", "local:test");

                Assert.IsTrue(authenticationResult.IsFail);
                Assert.AreEqual("There is no user with that username.", authenticationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_empty_username()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("", "test@user.com", "test_password", "test_password", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("Form data is incorrect.", registrationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_empty_email()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("test_user", "", "test_password", "test_password", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("Form data is incorrect.", registrationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_empty_password()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("test_user", "test@user.com", "", "test_password", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("Form data is incorrect.", registrationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_empty_confirm_password()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("test_user", "test@user.com", "test_password", "", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("Form data is incorrect.", registrationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_passworm_mismatch()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("test_user", "test@user.com", "test_password", "password_test", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("Passwords must be the same.", registrationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_user_already_exist()
        {
            BeginTest(() =>
            {
                SetupContextData(ApplicationUser.Create("already_registered", new byte[1], new byte[1], "registered@email.com").Value);
                var registrationResult = Component.Register("already_registered", "test@user.com", "test_password", "test_password", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("There is already user with that name.", registrationResult.Error);
            });
        }

        [Test, Order(2)]
        public void register_validate_email_already_exist()
        {
            BeginTest(() =>
            {
                SetupContextData(ApplicationUser.Create("already_registered", new byte[1], new byte[1], "registered@email.com").Value);
                var registrationResult = Component.Register("test_user", "registered@email.com", "test_password", "test_password", "local:test");

                Assert.IsTrue(registrationResult.IsFail);
                Assert.AreEqual("There is already registered account on that e-mail.", registrationResult.Error);
            });
        }

        [Test, Order(3)]
        public void can_register_user()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("test_user", "test@email.com", "test_password", "test_password", "local:test");

                Assert.IsTrue(registrationResult.IsOk);
            });
        }

        [Test, Order(4)]
        public void can_authenticate()
        {
            BeginTest(() =>
            {
                var registrationResult = Component.Register("test_user", "test@email.com", "test_password", "test_password", "local:test");
                var authenticationResult = Component.Authenticate("test_user", "test_password", "local:test");

                Assert.IsTrue(authenticationResult.IsOk);
            });
        }

        [Test, Order(5)]
        public void can_refresh_token()
        {

        }
    }
}
