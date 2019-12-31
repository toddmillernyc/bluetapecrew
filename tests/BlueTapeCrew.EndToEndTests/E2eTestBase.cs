using BlueTapeCrew.EndToEndTests.Models;
using Dapper;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using BlueTapeCrew.EndToEndTests.Helpers;

namespace BlueTapeCrew.EndToEndTests
{
    public class E2ETestBase : IDisposable
    {
        public const string DeadLetterPath = "C:\\SMTP\\DeadLetter";
        private const string PaypalSettingsPath = "C:\\config\\paypalsettings.json";

        public PaypalSettings PaypalSettings;

        public string Password = "Password123!";
        public const string Email = "bluetapecrew@mailinator.com";
        public string BaseUrl;

        public static RemoteWebDriver Driver;
        public static EndToEndTestHelper Helper;

        private static string TestRunId = Guid.NewGuid().ToString().Substring(0, 5);
        private static string _connectionString;

        public E2ETestBase()
        {
            var configJson = File.ReadAllText("testsettings.json");
            var settings = JsonConvert.DeserializeObject<TestSettings>(configJson);
            PaypalSettings = JsonConvert.DeserializeObject<PaypalSettings>(File.ReadAllText(PaypalSettingsPath));

            _connectionString = settings.ConnectionString;
            BaseUrl = settings.BaseUrl;
            
            Helper = new EndToEndTestHelper(_connectionString);
            
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Driver.Manage().Window.Maximize();
            
        }

        public void Dispose()
        {
            Cleanup();
            Driver.Close();
            Driver.Dispose();
        }


        protected async Task Cleanup()
        {
            await using var conn = new SqlConnection(_connectionString);

            //deletes
            var queries = new List<string>()
            {
                "DELETE FROM dbo.Cart",
                "DELETE FROM dbo.Styles",
                "DELETE FROM dbo.ProductCategories",
                "DELETE FROM dbo.ProductImages",
                "DELETE FROM dbo.AspNetUsers",
                "DELETE FROM dbo.AspNetUserRoles",
                "DELETE FROM dbo.AspNetRoles",
                "DELETE FROM dbo.ProductCategories",
                "DELETE FROM dbo.Categories",
                "DELETE FROM dbo.Colors",
                "DELETE FROM dbo.Sizes",
                "DELETE FROM dbo.Products",
                "DELETE FROM dbo.Images",
                "DELETE FROM dbo.SiteSettings",
                "DELETE FROM dbo.OrderItems",
                "DELETE FROM dbo.Orders"
            };
            foreach (var query in queries)
            {
                await conn.ExecuteAsync(query);
            }


            //erase dead letter files
            var di = new DirectoryInfo(DeadLetterPath);
            foreach (var file in di.GetFiles()) { file.Delete(); }
        }
    }
}
