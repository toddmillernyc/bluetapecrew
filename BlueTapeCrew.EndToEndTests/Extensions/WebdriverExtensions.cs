using System.Collections.Generic;
using OpenQA.Selenium;

namespace BlueTapeCrew.EndToEndTests.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebDriver FillForm(this IWebDriver driver, IDictionary<string, string> formDictionary)
        {
            foreach (var (key, value) in formDictionary)
            {
                driver.FindElement(By.Id(key)).SendKeys(value);
            }
            return driver;
        }
    }
}
