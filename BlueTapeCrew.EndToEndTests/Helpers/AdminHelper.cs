using BlueTapeCrew.EndToEndTests.Extensions;
using OpenQA.Selenium;

namespace BlueTapeCrew.EndToEndTests.Helpers
{
    public class AdminHelper
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;
        private readonly string _adminUser;
        private readonly string _adminPass;

        public AdminHelper(IWebDriver driver, string baseUrl, string adminUser, string adminPass)
        {
            _driver = driver;
            _baseUrl = baseUrl;
            _adminUser = adminUser;
            _adminPass = adminPass;
        }

        public void RemoveAllIntegrationTestUsers()
        {
            _driver.ClickId("loginLink");
            _driver.Fill("Email", _adminUser);
            _driver.PasswordFill("Password", _adminPass);
            _driver.ClickId("submitLogin");
            _driver.ClickId("adminLogin");
            _driver.ClickId("manageSiteUsers");
            while (true)
            {
                if(!DeleteNextIntegrationTestUser()) break;
            }
            ;
        }

        public bool DeleteNextIntegrationTestUser()
        {
            _driver.Wait(5);
            var rows = _driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
           foreach (var row in rows)
           {
               var rowId = row.GetAttribute("id");
               if (row.Text.Contains("integration-test-user"))
               {
                   _driver.Navigate().GoToUrl($"{_baseUrl}/admin/aspnetusers/delete/{rowId}");
                   _driver.ClickId("delete");
                   return true;
               }
           }
           return false;
        }

    }
}
