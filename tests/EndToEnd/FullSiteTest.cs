using System;
using System.Threading;
using System.Threading.Tasks;
using EndToEnd.Extensions;
using EndToEnd.Stubs;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace EndToEnd
{
    public class FullSiteTest : E2ETestBase
    {
        [Fact]
        public async Task LoginTest()
        {
            try
            {
                GoHome();
                RegisterUser();
                await ConfirmEmailAndLogIn();
                var userId = UpdateAccountInfo();
                await ResetPassword();
                await Helper.SeedAdminRole(TestSettings.Email);
                LogOffAndLogBackOn();
                _logger.Information("Navigating to Admin");
                Driver.FindElementById("adminLogin").Click();
                UpdateSiteSettings();
                AddCategories();
                AddProducts();
                ViewProductsAndAddToCart();
                Checkout();
                GuestCheckout();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "End To End Test Exception");
                throw;
            }
            finally
            {
                await Cleanup();
            }

        }

        private static void GoHome()
        {
            _logger.Information("Navigating to Homepage");
            Driver.Navigate().GoToUrl(TestSettings.BaseUrl); ;
        }

        private static void RegisterUser()
        {
            _logger.Information("Registering User");
            Driver.FindElementById("manage-account-header-link").Click();
            Driver.FindElementById("register-account-link").Click();
            Driver.FindElementById("Email").SendKeys(TestSettings.Email);
            Driver.FindElementByName("Password").SendKeys(TestSettings.EmailPassword);
            Driver.FindElementByName("ConfirmPassword").SendKeys(TestSettings.EmailPassword);
            Driver.FindElementById("register-account-submit-button").Click();
        }

        private static async Task ConfirmEmailAndLogIn()
        {
            _logger.Information("Confirm email an Log in");
            var confirmEmailLink = await Helper.GetConfirmEmailFromDeadLetterDirectory(TestSettings);
            Driver.Navigate().GoToUrl(confirmEmailLink);
            Driver.FindElementById("click-here-to-login-link").Click();
            Driver.FindElementById("Email").SendKeys(TestSettings.Email);
            Driver.FindElementById("Password").SendKeys(TestSettings.EmailPassword);
            Driver.FindElementById("submitLogin").Click();
        }

        private string UpdateAccountInfo()
        {
            _logger.Information("Update user account info");
            Driver.FindElementById("manage-account-header-link").Click();
            Driver.FillForm(_formDictionary);
            Driver.FindElementById("update-account-info-button").Click();
            Driver.Navigate().Refresh();
            foreach (var (key, expected) in _formDictionary)
            {
                var actual = Driver.FindElementById(key).GetAttribute("value");
                Assert.Equal(expected, actual);
            }
            var id = Driver.FindElementById("User_Id").GetAttribute("value");
            return id;
        }

        private static async Task ResetPassword()
        {
            _logger.Information("Reset password");
            Driver.ClickId("logoff").ClickId("loginLink").ClickId("forgot-password-link");
            Driver.FindElementById("Email").SendKeys(TestSettings.Email);
            Driver.ClickId("send-reset-password-email-button");
            var passwordResetLink = await Helper.GetConfirmEmailFromDeadLetterDirectory(TestSettings);
            Driver.Navigate().GoToUrl(passwordResetLink);
            TestSettings.EmailPassword = "NewPassword123!";
            Driver.FindElementById("Email").SendKeys(TestSettings.Email);
            Driver.FindElementById("Password").SendKeys(TestSettings.EmailPassword);
            Driver.FindElementById("ConfirmPassword").SendKeys(TestSettings.EmailPassword);
            Driver.ClickId("reset-password-submit-button");
            Helper.Login(Driver, TestSettings);
        }

        private static void LogOffAndLogBackOn()
        {
            _logger.Information("Log out and log back in");
            Driver.ClickId("logoff");
            Helper.Login(Driver, TestSettings);
        }

        private void UpdateSiteSettings()
        {
            _logger.Information("Update site settings");
            Driver.FindElementById("paypal-client-id").SendKeys(PaypalSettings.ClientId);
            Driver.FindElementById("paypal-sandbox-client-id").SendKeys(PaypalSettings.ClientId);
            Driver.FindElementById("paypal-client-secret").SendKeys(PaypalSettings.ClientSecret);
            Driver.FindElementById("paypal-sandbox-client-secret").SendKeys(PaypalSettings.ClientSecret);
            Driver.FindElementById("title").SendKeys("End to End");
            Driver.FindElementById("keywords").SendKeys("functional testing tests");
            Driver.FindElementById("description").SendKeys("This is a site  created by an automated, full site end to end test.");
            Driver.ClickId("save-site-settings-button");

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(1));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            var alert = Driver.SwitchTo().Alert();
            alert.Accept();

            var top = Driver.FindElementById("edit-categories-link");
            var actions = new Actions(Driver);
            actions.MoveToElement(top);
            actions.Perform();
        }

        private static void AddCategories()
        {
            _logger.Information("Add categories");
            Driver.FindElementById("edit-categories-link").Click();
            foreach (var category in StringStubs.Categories)
            {
                Driver.FindElementById("new-category-name-input").SendKeys(category);
                Driver.FindElementById("submit-new-category-button").Click();
            }
            Driver.FindElementById("return-to-products-link").Click();
        }

        private static void AddProducts()
        {
            _logger.Information("Add products");
            foreach (var product in ProductStubs.Products) Helper.AddProduct(Driver, product);
            Thread.Sleep(100);
            Driver.ClickId("return-to-site-link");
        }

        private static void ViewProductsAndAddToCart()
        {
            _logger.Information("View Products and Add to Cart");
            const int productIndex = 0;
            Helper.ProductCardZoomAndOpen(Driver, productIndex);
            Driver.ClickId("add-to-cart-button");
            Driver.Hover("cart-widget");
            Driver.ClickId("go-to-checkout-button");
        }

        private void Checkout()
        {
            _logger.Information("Checkout");
            Driver.ClickId("shippingInfo");
            Driver.ClickId("button-payment-address");
            Driver.ClickId("button-confirm");
            Driver.FindElementById("email").SendKeys(PaypalSettings.PaypalBuyer);
            Driver.ClickId("btnNext");
            Driver.FindElementById("password").SendKeys(PaypalSettings.PaypalBuyerPassword);
            Driver.ClickId("btnLogin");
            Thread.Sleep(5000);
            Driver.ClickId("payment-submit-btn");
            Thread.Sleep(4000);
            Driver.ClickId("confirm-order-button");  //site
            Driver.ClickId("manage-account-header-link");
        }

        private static void GuestCheckout()
        {
            _logger.Information("Guest Checkout");
            Driver.ClickId("logoff");
            Driver.ClickId("product-details-link-0");
            Driver.ClickId("add-to-cart-button");
            Driver.ClickId("cart-header-link");
            Driver.ClickId("cart-checkout-link");
            Driver.ClickId("guest-checkout-button");
            Driver.FindElementById("firstname").SendKeys("Guest");
            Driver.FindElementById("lastname").SendKeys("User");
            Driver.FindElementById("email").SendKeys("btguestuser@mailinator.com");
            Driver.FindElementById("telephone").SendKeys("212-222-3333");
            Driver.FindElementById("address").SendKeys("120 1st Ave.");
            Driver.FindElementById("city").SendKeys("Manhattan");
            Driver.FindElementById("state").SendKeys("NY");
            Driver.FindElementByName("PostalCode").SendKeys("10021");
            Driver.FindElementById("button-payment-address").Click();
            Thread.Sleep(1000);
            Driver.ClickId("button-confirm");
            Thread.Sleep(3000);
            Driver.ClickId("payment-submit-btn");
            Thread.Sleep(4000);
            Driver.ClickId("confirm-order-button");
            Driver.ClickId("manage-account-header-link");
        }
    }
}
