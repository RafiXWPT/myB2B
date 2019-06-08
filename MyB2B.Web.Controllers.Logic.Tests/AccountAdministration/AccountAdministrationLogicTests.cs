using MyB2B.Domain.Companies;
using MyB2B.Domain.Identity;
using MyB2B.Web.Controllers.Logic.AccountAdministration;
using MyB2B.Web.Controllers.Logic.AccountAdministration.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyB2B.Web.Controllers.Logic.Tests.AccountAdministration
{
    [TestFixture]
    public class AccountAdministrationLogicTests : LogicTest<AccountAdministrationLogic>
    {
        [Test]
        public void can_get_empty_company_data()
        {
            BeginTest(() =>
            {
                var testUser = ApplicationUser.Create("test_user", new byte[1], new byte[1], "test@email.com").Value;
                SetupContextData(testUser);

                var result = Component.GetAccountCompanyData(testUser.Id);

                Assert.IsTrue(result.IsOk);
            });
        }

        [Test]
        public void wont_get_company_for_non_existing_user()
        {
            BeginTest(() =>
            {
                var result = Component.GetAccountCompanyData(1);

                Assert.IsTrue(result.IsFail);
                Assert.AreEqual("There is no user with that id.", result.Error);
            });
        }

        [Test]
        public void can_update_user_company()
        {
            BeginTest(() =>
            {
                var testUser = ApplicationUser.Create("test_user", new byte[1], new byte[1], "test@email.com").Value;
                SetupContextData(testUser);

                var result = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TC",
                    CompanyNip = "1234567890",
                    CompanyRegon = "123456789",
                    Country = "Poland",
                    City = "Cracow",
                    ZipCode = "30-000",
                    Street = "Cracovia",
                    Number = "1"
                });

                Assert.IsTrue(result.IsOk);
            });
        }

        [Test]
        public void cant_update_company_on_validations()
        {
            BeginTest(() =>
            {
                var testUser = ApplicationUser.Create("test_user", new byte[1], new byte[1], "test@email.com").Value;
                SetupContextData(testUser);

                var emptyCompanyNameResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "",
                    ShortCode = ""
                });

                var tooShortCompanyNameResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "1",
                    ShortCode = ""
                });

                var tooLongCompanyNameResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = string.Join("", Enumerable.Range(1, 256).ToList()),
                    ShortCode = ""
                });

                var emptyShortCodeResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = ""
                });

                var tooShortShortCodeResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "1"
                });

                var tooLongShortCodeResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = string.Join("", Enumerable.Range(1, 11).ToList())
                });

                var emptyNipResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TR",
                    CompanyNip = "",
                    CompanyRegon = ""
                });

                var tooShortNipLengthResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TR",
                    CompanyNip = "123456789",
                    CompanyRegon = ""
                });

                var tooLongNipLengthResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TR",
                    CompanyNip = "12345678901",
                    CompanyRegon = ""
                });

                var emptyRegonResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TR",
                    CompanyNip = "1234567890",
                    CompanyRegon = ""
                });

                var tooShortRegonLengthResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TR",
                    CompanyNip = "1234567890",
                    CompanyRegon = "12345678"
                });

                var tooLongRegonLengthResult = Component.UpdateCompanyData(testUser.Id, new AccountCompanyDataDto
                {
                    CompanyName = "Test Company",
                    ShortCode = "TR",
                    CompanyNip = "1234567890",
                    CompanyRegon = "1234567890"
                });

                Assert.IsTrue(emptyCompanyNameResult.IsFail);
                Assert.AreEqual("Company name length must be between 3-255 characters long.", emptyCompanyNameResult.Error);
                Assert.IsTrue(tooShortCompanyNameResult.IsFail);
                Assert.AreEqual("Company name length must be between 3-255 characters long.", tooShortCompanyNameResult.Error);
                Assert.IsTrue(tooLongCompanyNameResult.IsFail);
                Assert.AreEqual("Company name length must be between 3-255 characters long.", tooLongCompanyNameResult.Error);
                Assert.IsTrue(emptyShortCodeResult.IsFail);
                Assert.AreEqual("Company short code length must be between 2-10 characters long.", emptyShortCodeResult.Error);
                Assert.IsTrue(tooShortShortCodeResult.IsFail);
                Assert.AreEqual("Company short code length must be between 2-10 characters long.", tooShortShortCodeResult.Error);
                Assert.IsTrue(tooLongShortCodeResult.IsFail);
                Assert.AreEqual("Company short code length must be between 2-10 characters long.", tooLongShortCodeResult.Error);
                Assert.IsTrue(emptyNipResult.IsFail);
                Assert.AreEqual("Nip identifier must have 10 digits", emptyNipResult.Error);
                Assert.IsTrue(tooShortNipLengthResult.IsFail);
                Assert.AreEqual("Nip identifier must have 10 digits", tooShortNipLengthResult.Error);
                Assert.IsTrue(tooLongNipLengthResult.IsFail);
                Assert.AreEqual("Nip identifier must have 10 digits", tooLongNipLengthResult.Error);
                Assert.IsTrue(emptyRegonResult.IsFail);
                Assert.AreEqual("Regon identifier must have 10 digits", emptyRegonResult.Error);
                Assert.IsTrue(tooShortRegonLengthResult.IsFail);
                Assert.AreEqual("Regon identifier must have 10 digits", tooShortRegonLengthResult.Error);
                Assert.IsTrue(tooLongRegonLengthResult.IsFail);
                Assert.AreEqual("Regon identifier must have 10 digits", tooLongRegonLengthResult.Error);
            });
        }
    }
}
