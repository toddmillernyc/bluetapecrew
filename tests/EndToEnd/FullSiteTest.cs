using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndToEnd.Extensions;
using EndToEnd.Models;
using EndToEnd.Stubs;
using Newtonsoft.Json;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace EndToEnd
{
    public class FullSiteTest : E2ETestBase
    {
        private const int WaitSeconds = 10;
        private readonly Dictionary<string, string> _formDictionary = new Dictionary<string, string>()
        {
            {"User_FirstName", "John"},{"User_LastName", "Smith"},{"User_PhoneNumber", "555-555-5555"},
            {"User_Address", "123 Any Street"},{"User_City", "AnyTown"},{"User_State", "NY"},{"User_PostalCode", "10001"}
        };

        [Fact]
        public async Task LoginTest()
        {
            try
            {
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(WaitSeconds);
                GoHome();
                RegisterUser();
                await ConfirmEmailAndLogIn();
                var userId = UpdateAccountInfo();
                await ResetPassword();
                await Helper.SeedAdminRole(Email);
                LogOffAndLogBackOn();
                Driver.FindElementById("adminLogin").Click();
                UpdateSiteSettings();
                AddCategories();
                AddProducts();
                ViewProductsAndAddToCart();
                Checkout();
                GuestCheckout();
            }
            finally
            {
                await Cleanup();
            }

        }

        private void Checkout()
        {
            Driver.ClickId("shippingInfo");
            Driver.ClickId("button-payment-address");
            Driver.ClickId("button-confirm");
            Driver.FindElementById("email").SendKeys(PaypalSettings.PaypalBuyer);
            Driver.ClickId("btnNext");
            Driver.FindElementById("password").SendKeys(PaypalSettings.PaypalBuyerPassword);
            Driver.ClickId("btnLogin");
            Thread.Sleep(3000);
            Driver.ClickId("payment-submit-btn");
            Thread.Sleep(4000);
            Driver.ClickId("confirm-order-button");  //site
            Driver.ClickId("manage-account-header-link");
        }

        private static void GuestCheckout()
        {
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



        private static void ViewProductsAndAddToCart()
        {
            var productIndex = 0;
            ProductCardZoomAndOpen(productIndex);
            Driver.ClickId("add-to-cart-button");
            Driver.Hover("cart-widget");
            Driver.ClickId("go-to-checkout-button");
        }

        private static void AddProducts()
        {
            foreach (var product in ProductStubs.Products) AddProduct(product);
            Thread.Sleep(100);
            Driver.ClickId("return-to-site-link");
        }

        private static void AddProduct(ProductStub stub)
        {
            Driver.ClickId("create-new-product-link");
            Driver.FindElementById("ProductName").SendKeys(stub.Name);
            Driver.FindElementById("Slug").SendKeys(stub.Slug);
            Driver.FindElementById("Description").SendKeys(stub.Description);
            Driver.SelectDropdownText("CategoryId", stub.Category);

            var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", stub.Image + ".jpg");
            Driver.FindElementById("product-image-file-chooser").SendKeys(imageFilePath);
            Driver.ClickId("submit-new-product-button");

            foreach (var color in stub.Colors)
            {
                Driver.FindElementByName("newColor").SendKeys(color);
                Driver.ClickId("submit-add-color-button");
            }

            foreach (var size in stub.Sizes)
            {
                Driver.FindElementByName("size").SendKeys(size);
                Driver.ClickId("add-size-submit-button");

                foreach (var color in stub.Colors)
                {
                    Driver.SelectDropdownText("ColorId", color);
                    Driver.SelectDropdownText("SizeId", size);
                    Driver.FindElementByName("Price").SendKeys(stub.Price.ToString(CultureInfo.InvariantCulture));
                    Driver.ClickId("add-style-submit-button");
                }
            }

            Driver.FindElementById("additional-image-chooser").SendKeys(imageFilePath);
            Driver.ClickId("upload-additional-image-button");
        }

        private void UpdateSiteSettings()
        {
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

        private static void ProductCardZoomAndOpen(int index)
        {
            Driver.Hover("product-image-wrapper-0").ClickId($"fancybox-button-{index}");
            Thread.Sleep(250);
            Driver.FindElementByClassName("fancybox-close").Click();
            Driver.ClickId($"product-details-link-{index}");
        }

        private async Task ResetPassword()
        {
            Driver.ClickId("logoff").ClickId("loginLink").ClickId("forgot-password-link");
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.ClickId("send-reset-password-email-button");
            var passwordResetLink = await GetConfirmEmailFromDeadLetterDirectory();
            Driver.Navigate().GoToUrl(passwordResetLink);
            Password = "NewPassword123!";
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementById("Password").SendKeys(Password);
            Driver.FindElementById("ConfirmPassword").SendKeys(Password);
            Driver.ClickId("reset-password-submit-button");
            Login();
        }

        private static void AddCategories()
        {
            Driver.FindElementById("edit-categories-link").Click();
            foreach (var category in StringStubs.Categories)
            {
                Driver.FindElementById("new-category-name-input").SendKeys(category);
                Driver.FindElementById("submit-new-category-button").Click();
            }
            Driver.FindElementById("return-to-products-link").Click();
        }

        private void LogOffAndLogBackOn()
        {
            Driver.ClickId("logoff");
            Login();
        }

        private void Login()
        {
            Driver.FindElementById("loginLink").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementById("Password").SendKeys(Password);
            Driver.FindElementById("submitLogin").Click();
        }

        private string UpdateAccountInfo()
        {
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

        private async Task ConfirmEmailAndLogIn()
        {
            var confirmEmailLink = await GetConfirmEmailFromDeadLetterDirectory();
            Driver.Navigate().GoToUrl(confirmEmailLink);
            Driver.FindElementById("click-here-to-login-link").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementById("Password").SendKeys(Password);
            Driver.FindElementById("submitLogin").Click();
        }

        private static async Task<string> GetConfirmEmailFromDeadLetterDirectory()
        {
            var directory = new DirectoryInfo(DeadLetterPath);
            var file = directory.GetFiles().OrderByDescending(x => x.LastWriteTime).First();
            var deadLetterJson = await File.ReadAllTextAsync(file.FullName);
            var email = JsonConvert.DeserializeObject<DeadLetter>(deadLetterJson);
            return email.TextBody;
        }

        private void RegisterUser()
        {
            Driver.FindElementById("manage-account-header-link").Click();
            Driver.FindElementById("register-account-link").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementByName("Password").SendKeys(Password);
            Driver.FindElementByName("ConfirmPassword").SendKeys(Password);
            Driver.FindElementById("register-account-submit-button").Click();
        }

        private void GoHome()
        {
            Driver.Navigate().GoToUrl(BaseUrl);;
        }
    }
}
