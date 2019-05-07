using System;
using OpenQA.Selenium;

namespace BlueTapeCrew.EndToEndTests.Extensions
{
    public static class WebDriverExtensions
    {
        public static void ClickId(this IWebDriver driver, string id) => driver.FindElement(By.Id(id)).Click();

        public static void ClickX(this IWebDriver driver, string xPath) => driver.FindElement(By.XPath(xPath)).Click();

        public static void Fill(this IWebDriver driver, string id, string text) => driver.FindElement(By.Id(id)).SendKeys(text);

        public static void Goto(this IWebDriver driver, string url) => driver.Navigate().GoToUrl(url);

        public static void PasswordFill(this IWebDriver driver, string id, string text)
        {
            var element = driver.FindElement(By.Id(id));
            foreach (var c in text)
                element.SendKeys(c.ToString());
        }

        public static void SwitchToFrame(this IWebDriver driver, string id) => driver.SwitchTo().Frame(driver.FindElement(By.Id(id)));

        public static IWebElement Tag(this IWebDriver driver, string tagName) => driver.FindElement(By.TagName("a"));

        public static string Value(this IWebDriver driver, string id) =>
            driver.FindElement(By.Id(id)).GetAttribute("value");

        public static void Wait(this IWebDriver driver, int seconds) => driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
    }
}
