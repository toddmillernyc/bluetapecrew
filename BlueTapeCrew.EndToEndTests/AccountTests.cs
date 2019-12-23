using BlueTapeCrew.EndToEndTests.Extensions;
using BlueTapeCrew.EndToEndTests.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlueTapeCrew.EndToEndTests.Stubs;
using Xunit;

namespace BlueTapeCrew.EndToEndTests
{
    public class AccountTests : EneToEndTestBase
    {
        [Fact]
        public async Task LoginTest()
        {
            RegisterUser();
            await ConfirmEmailAndLogIn();
            var userId = UpdateAccountInfo();
            await Helper.SeedAdminRole(Email);
            LogOffAndLogBackOn();
            AddCategories();

        }

        private static void AddCategories()
        {
            Driver.FindElementById("adminLogin").Click();
            Driver.FindElementById("edit-categories-link").Click();
            foreach (var category in CategoryStubs.Categories)
            {
                Driver.FindElementById("new-category-name-input").SendKeys(category);
                Driver.FindElementById("submit-new-category-button").Click();
            }
            Driver.FindElementById("return-to-products-link").Click();
        }

        private static void LogOffAndLogBackOn()
        {
            Driver.FindElementById("logoff").Click();
            Driver.FindElementById("loginLink").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementById("Password").SendKeys(Password);
            Driver.FindElementById("submitLogin").Click();
        }

        private static string UpdateAccountInfo()
        {
            var formDictionary = new Dictionary<string, string>()
            {
                {"User_FirstName", "John"},
                {"User_LastName", "Smith"},
                {"User_PhoneNumber", "555-555-5555"},
                {"User_Address", "123 Any Street"},
                {"User_City", "AnyTown"},
                {"User_State", "NY"},
                {"User_PostalCode", "10001"}
            };

            Driver.FindElementById("manage-account-header-link").Click();
            Driver.FillForm(formDictionary);
            Driver.FindElementById("update-account-info-button").Click();
            Driver.Navigate().Refresh();
            foreach (var (key, expected) in formDictionary)
            {
                var actual = Driver.FindElementById(key).GetAttribute("value");
                Assert.Equal(expected, actual);
            }
            var id = Driver.FindElementById("User_Id").GetAttribute("value");
            return id;
        }

        private static async Task ConfirmEmailAndLogIn()
        {
            var confirmEmailLink = await GetConfirmEmailFromDeadLetterDirectory();
            Driver.Navigate().GoToUrl(confirmEmailLink);
            Thread.Sleep(5);
            Driver.FindElementById("click-here-to-login-link").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementById("Password").SendKeys(Password);
            Driver.FindElementById("submitLogin").Click();
        }

        private static async Task<string> GetConfirmEmailFromDeadLetterDirectory()
        {
            var filePath = Directory.GetFiles(DeadLetterPath).FirstOrDefault();
            var deadLetterJson = await File.ReadAllTextAsync(filePath);
            var email = JsonConvert.DeserializeObject<DeadLetter>(deadLetterJson);
            return email.TextBody;
        }

        private void RegisterUser()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
            Driver.FindElementById("manage-account-header-link").Click();
            Driver.FindElementById("register-account-link").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementByName("Password").SendKeys(Password);
            Driver.FindElementByName("ConfirmPassword").SendKeys(Password);
            Driver.FindElementById("register-account-submit-button").Click();
        }
    }
}
