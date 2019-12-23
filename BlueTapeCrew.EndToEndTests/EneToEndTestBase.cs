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
    public class EneToEndTestBase : IDisposable
    {
        public const string DeadLetterPath = "C:\\SMTP\\DeadLetter";
        public const string Password = "Password123!";
        public const string Email = "bluetapecrew@mailinator.com";
        public string BaseUrl;

        public static RemoteWebDriver Driver;
        public static EndToEndTestHelper Helper;

        private static string TestRunId = Guid.NewGuid().ToString().Substring(0, 5);
        private static string _connectionString;

        public EneToEndTestBase()
        {
            var configJson = File.ReadAllText("testsettings.json");
            var settings = JsonConvert.DeserializeObject<TestSettings>(configJson);
            _connectionString = settings.ConnectionString;
            BaseUrl = settings.BaseUrl;
            Driver = new ChromeDriver();
            Helper = new EndToEndTestHelper(_connectionString);
            Driver.Manage().Window.FullScreen();
        }

        public void Dispose()
        {
            Cleanup(_connectionString);
            Driver.Close();
            Driver.Dispose();
        }


        private static async Task Cleanup(string connectionString)
        {
            await using var conn = new SqlConnection(connectionString);

            //deletes
            var queries = new List<string>()
            {
                "DELETE FROM dbo.AspNetUsers",
                "DELETE FROM dbo.AspNetUserRoles",
                "DELETE FROM dbo.AspNetRoles",
                "DELETE FROM dbo.Categories"
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
