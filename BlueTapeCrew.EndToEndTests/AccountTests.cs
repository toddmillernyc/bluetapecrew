using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            driver.Navigate().GoToUrl(settings.BaseUrl);

        }
    }
}
