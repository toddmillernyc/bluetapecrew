using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace EndToEnd.Extensions
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

        public static IWebDriver ClickId(this IWebDriver driver, string id)
        {
            var isPresent = driver.FindElement(By.Id(id));
            driver.FindElement(By.Id(id)).Click();
            return driver;
        }

        public static IWebDriver SelectDropdownText(this IWebDriver driver, string id, string text)
        {
            var categorySelect = new SelectElement(driver.FindElement(By.Id(id)));
            categorySelect.SelectByText(text);
            return driver;
        }

        public static IWebDriver Hover(this IWebDriver driver, string id)
        {
            var element = driver.FindElement(By.Id(id));
            var action = new Actions(driver);
            action.MoveToElement(element).Perform();
            return driver;
        }

        public static IWebDriver Esc(this IWebDriver driver)
        {
            var action = new Actions(driver);
            action.SendKeys(OpenQA.Selenium.Keys.Escape);
            return driver;
        }
    }
}
