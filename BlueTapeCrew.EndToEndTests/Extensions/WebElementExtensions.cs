using OpenQA.Selenium;

namespace BlueTapeCrew.EndToEndTests.Extensions
{
    public static class WebElementExtensions
    {
        public static string Href(this IWebElement element) => element.GetAttribute("href");
    }
}
