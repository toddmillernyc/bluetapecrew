using System;
using EndToEndTests.Extensions;
using EndToEndTests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace EndToEndTests
{
    [CollectionDefinition("EndToEnd")]
    public class IntegrationTest : ICollectionFixture<EndToEndFixture> { }

    public class EndToEndFixture : IDisposable
    {
        //must add this admin user manually
        private const string AdminUser = "btc_endtoend_admin@mailinator.com";
        private const string AdminPassword = "d77c41f9-11dd-441b-9cf2-d64f3b279adc";
        private const string BaseUrl = "https://localhost";

        public readonly IWebDriver Driver;

        public EndToEndFixture()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            Driver = new ChromeDriver(chromeOptions);
        }


        public void RegisterNewUserAndOpenAccountPage(string email, string password)
        {
            var helper = new RegistrationHelper(Driver, BaseUrl);
            helper.FillOutRegisterForm(email, password);
            var link = helper.GetConfirmEmailLinkFromMailinator(email);
            Driver.Goto(link);
            Driver.ClickId("loginLink");
            helper.Login(email, password);
            Driver.ClickId("myAccount");
        }

        public void RemoveAllIntegrationTestUsers()
        {
            var helper = new AdminHelper(Driver, BaseUrl, AdminUser, AdminPassword);
            helper.RemoveAllIntegrationTestUsers();
        }


        public void Dispose()
        {
            Driver?.Dispose();
        }
    }
}
