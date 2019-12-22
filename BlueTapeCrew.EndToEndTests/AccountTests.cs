using Dapper;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace BlueTapeCrew.EndToEndTests
{
    public class AccountTests
    {
        private readonly TestSettings _settings;
        private const string DeadLetterPath = "C:\\SMTP\\DeadLetter";

        private const string Password = "Password123!";
        private const string Email = "bluetapecrew@mailinator.com";

        public AccountTests()
        {
            var configJson = File.ReadAllText("testsettings.json");
            _settings = JsonConvert.DeserializeObject<TestSettings>(configJson);
        }

        [Fact]
        public async Task LoginTest()
        {

            var driver = new ChromeDriver();
            try
            {
                RegisterUser(driver);
                await ConfirmEmail(driver);

            }
            finally
            {
                await Cleanup(_settings.ConnectionString);
                //driver.Close();
                //driver.Dispose();
            }
        }

        private static async Task ConfirmEmail(RemoteWebDriver driver)
        {
            var confirmEmailLink = await GetConfirmEmailFromDeadLetterDirectory();
            driver.Navigate().GoToUrl(confirmEmailLink);
        }

        private static async Task<string> GetConfirmEmailFromDeadLetterDirectory()
        {
            var filePath = Directory.GetFiles(DeadLetterPath).FirstOrDefault();
            var deadLetterJson = await File.ReadAllTextAsync(filePath);
            var email = JsonConvert.DeserializeObject<DeadLetter>(deadLetterJson);
            var hrefLink = XElement.Parse(email.HtmlBody)
                .Descendants("a")
                .Select(x => x.Attribute("href")?.Value)
                .FirstOrDefault();
            return hrefLink;
        }

        private void RegisterUser(RemoteWebDriver driver)
        {
            driver.Navigate().GoToUrl(_settings.BaseUrl);
            driver.FindElementById("myAccount").Click();
            driver.FindElement(By.XPath("//*[@id=\"registerAccount\"]/a")).Click();
            driver.FindElementById("Email").SendKeys(Email);
            driver.FindElementByName("Password").SendKeys(Password);
            driver.FindElementByName("ConfirmPassword").SendKeys(Password);
            driver.FindElementById("register").Click();
        }


        private static async Task Cleanup(string connectionString)
        {
            await using var conn = new SqlConnection(connectionString);
            
            //delete users
            await conn.ExecuteAsync("DELETE FROM dbo.AspNetUsers");
            
            //erase dead letter files
            var di = new DirectoryInfo(DeadLetterPath);
            foreach (var file in di.GetFiles()) { file.Delete(); }
        }
    }
}
