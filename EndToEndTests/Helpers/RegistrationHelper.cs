using EndToEndTests.Extensions;
using OpenQA.Selenium;

namespace EndToEndTests.Helpers
{
    public class RegistrationHelper
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;

        public RegistrationHelper(IWebDriver driver, string baseUrl)
        {
            _driver = driver;
            _baseUrl = baseUrl;
        }

        public string GetConfirmEmailLinkFromMailinator(string userEmail)
        {
            var username = userEmail.Split("@")[0];
            var url =
                $"https://www.mailinator.com/v3/index.jsp?zone=public&query={username}#/#inboxpane";
            _driver.Goto(url);
            _driver.Wait(2);
            _driver.ClickX("//td[contains(text(),'bluetapecrew@gmail.com')]");
            _driver.SwitchToFrame("msg_body");
            _driver.Wait(1);
            var href = _driver.Tag("a").Href();
            _driver.SwitchTo().ParentFrame();
            _driver.ClickId("trash_but");
            return href;
        }

        public void FillOutRegisterForm(string email, string password)
        {
            _driver.Navigate().GoToUrl($"{_baseUrl}/");
            _driver.ClickId("myAccount");
            _driver.ClickId("registerAccount");
            _driver.Fill("Email", email);
            _driver.PasswordFill("Password", password);
            _driver.PasswordFill("ConfirmPassword", password);
            _driver.ClickId("register");
        }

        public void Login(string email, string password)
        {
            _driver.Fill("Email", email);
            _driver.PasswordFill("Password", password);
            _driver.ClickId("submitLogin");
        }
    }
}
