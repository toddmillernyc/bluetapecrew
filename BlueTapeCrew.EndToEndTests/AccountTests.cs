using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BlueTapeCrew.EndToEndTests
{
    public class AccountTests
    {
        [Fact]
        public async Task LoginTest()
        {
            var configJson = await File.ReadAllTextAsync("testsettings.json");
            var settings = JsonConvert.DeserializeObject<TestSettings>(configJson);
            var driver = new ChromeDriver();
            try
            {
                var password = "Password123!";
                var email = "bluetapecrew@mailinator.com";

                driver.Navigate().GoToUrl(settings.BaseUrl);
                driver.FindElementById("myAccount").Click();
                driver.FindElement(By.XPath("//*[@id=\"registerAccount\"]/a")).Click();
                driver.FindElementById("Email").SendKeys(email);
                driver.FindElementByName("Password").SendKeys(password);
                driver.FindElementByName("ConfirmPassword").SendKeys(password);
                driver.FindElementById("register").Click();
            }
            finally
            {
                await Cleanup(settings.ConnectionString);
                //driver.Close();
                //driver.Dispose();
            }
        }


        private static async Task Cleanup(string connectionString)
        {
            await using var conn = new SqlConnection(connectionString);
            await conn.ExecuteAsync("DELETE FROM dbo.AspNetUsers");
        }
    }
}
