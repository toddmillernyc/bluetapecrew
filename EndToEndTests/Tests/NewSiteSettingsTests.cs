using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using EndToEndTests.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace EndToEndTests.Tests
{
    public class NewSiteSettingsTests
    {
        [Fact]
        public void SubmitSiteSettings()
        {
            //arrange
            var baseUrl = "http://localhost:3000";
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            var driver = new ChromeDriver(chromeOptions);
            var values = new Dictionary<string, string>();
            var random = new Random();

            //act
            driver.Navigate().GoToUrl(baseUrl);
            foreach (var input in driver.FindElements(By.TagName("input")).Concat(driver.FindElements(By.TagName("textarea"))))
            {
                var name = input.GetAttribute("name");
                if (!string.IsNullOrEmpty(name))
                {
                    
                    var fillText = Guid.NewGuid().ToString();

                    if (name == "smtpPort" || name == "freeShippingThreshold" || name == "flatShippingRate")
                        fillText = random.Next(1080, 15000).ToString();
                    values[name] = fillText;
                    input.Clear();
                    input.SendKeys(fillText);
                }
                else
                {
                    Debug.WriteLine(name);
                }
            }
            driver.FindElement(By.Id("saveButton")).Click();
            driver.Goto(baseUrl);

            //assert
            foreach (var kvp in values)
            {
                var expected = values[kvp.Key];
                var actual = driver.FindElementByName(kvp.Key).GetAttribute("value");
                Assert.Equal(expected, actual);
            }
        }
    }
}
