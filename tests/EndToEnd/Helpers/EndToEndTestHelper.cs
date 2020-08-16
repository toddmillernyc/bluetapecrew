using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using EndToEnd.Extensions;
using EndToEnd.Models;
using EndToEnd.Stubs;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using Serilog;

namespace EndToEnd.Helpers
{
    public class EndToEndTestHelper
    {
        private static string _connectionString;
        private static ILogger _logger;

        public EndToEndTestHelper(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task SeedAdminRole(string userEmail)
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.ExecuteAsync(
                "INSERT INTO dbo.AspNetRoles(Id,[Name],NormalizedName,ConcurrencyStamp) VALUES(NEWID(),'Admin','ADMIN',NEWID())");
            var roleId = (await conn.QueryAsync<string>("SELECT TOP 1 Id FROM dbo.AspNetRoles WHERE [Name] = 'Admin'")).FirstOrDefault();
            var userId = (await conn.QueryAsync<string>($"SELECT TOP 1 Id FROM dbo.AspNetUsers WHERE Email = '{userEmail}'")).FirstOrDefault();
            await conn.ExecuteAsync($"INSERT INTO dbo.AspNetUserRoles(UserId,RoleId) VALUES('{userId}','{roleId}')");
        }

        public void AddProduct(RemoteWebDriver driver, ProductStub stub)
        {
            _logger.Information($"Add Product {stub.Name}");
            driver.ClickId("create-new-product-link");
            driver.FindElementById("ProductName").SendKeys(stub.Name);
            driver.FindElementById("Slug").SendKeys(stub.Slug);
            driver.FindElementById("Description").SendKeys(stub.Description);
            driver.SelectDropdownText("CategoryId", stub.Category);

            var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", stub.Image + ".jpg");
            driver.FindElementById("product-image-file-chooser").SendKeys(imageFilePath);
            driver.ClickId("submit-new-product-button");

            foreach (var color in stub.Colors)
            {
                driver.FindElementByName("newColor").SendKeys(color);
                driver.ClickId("submit-add-color-button");
            }

            foreach (var size in stub.Sizes)
            {
                driver.FindElementByName("size").SendKeys(size);
                driver.ClickId("add-size-submit-button");

                foreach (var color in stub.Colors)
                {
                    driver.SelectDropdownText("ColorId", color);
                    driver.SelectDropdownText("SizeId", size);
                    driver.FindElementByName("Price").SendKeys(stub.Price.ToString(CultureInfo.InvariantCulture));
                    driver.ClickId("add-style-submit-button");
                }
            }

            driver.FindElementById("additional-image-chooser").SendKeys(imageFilePath);
            driver.ClickId("upload-additional-image-button");
        }

        public void ProductCardZoomAndOpen(RemoteWebDriver driver, int index)
        {
            driver.Hover("product-image-wrapper-0").ClickId($"fancybox-button-{index}");
            Thread.Sleep(250);
            driver.FindElementByClassName("fancybox-close").Click();
            driver.ClickId($"product-details-link-{index}");
        }

        public void Login(RemoteWebDriver driver, TestSettings settings)
        {
            driver.FindElementById("loginLink").Click();
            driver.FindElementById("Email").SendKeys(settings.Email);
            driver.FindElementById("Password").SendKeys(settings.EmailPassword);
            driver.FindElementById("submitLogin").Click();
        }

        public async Task<string> GetConfirmEmailFromDeadLetterDirectory(TestSettings settings)
        {
            var directory = new DirectoryInfo(settings.DeadLetterPath);
            var file = directory.GetFiles().OrderByDescending(x => x.LastWriteTime).First();
            var deadLetterJson = await File.ReadAllTextAsync(file.FullName);
            var email = JsonConvert.DeserializeObject<DeadLetter>(deadLetterJson);
            return email.TextBody;
        }
    }
}
