using OpenQA.Selenium;

namespace EndToEndTests.Extensions
{
    public static class WebElementExtensions
    {
        public static string Href(this IWebElement element) => element.GetAttribute("href");
    }
}
