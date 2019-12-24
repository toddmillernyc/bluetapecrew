using BlueTapeCrew.EndToEndTests.Extensions;
using BlueTapeCrew.EndToEndTests.Models;
using BlueTapeCrew.EndToEndTests.Stubs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlueTapeCrew.EndToEndTests
{
    public class FullSiteTest : E2ETestBase
    {
        readonly Dictionary<string, string> _formDictionary = new Dictionary<string, string>()
        {
            {"User_FirstName", "John"},
            {"User_LastName", "Smith"},
            {"User_PhoneNumber", "555-555-5555"},
            {"User_Address", "123 Any Street"},
            {"User_City", "AnyTown"},
            {"User_State", "NY"},
            {"User_PostalCode", "10001"}
        };

        [Fact]
        public async Task LoginTest()
        {
            var tryCount = 0;

            while (tryCount < 2)
            {
                try
                {
                    RegisterUser();
                    await ConfirmEmailAndLogIn();
                    var userId = UpdateAccountInfo();
                    await ResetPassword();
                    await Helper.SeedAdminRole(Email);
                    LogOffAndLogBackOn();
                    AddCategories();

                    foreach (var product in ProductStubs.Products)
                        AddProduct(product);

                    Driver.ClickId("return-to-site-link");

                    var productIndex = 0;
                    ProductCardZoomAndOpen(productIndex);
                    Driver.ClickId("add-to-cart-button");
                    Driver.Hover("cart-widget");
                    Driver.ClickId("go-to-checkout-button");
                    break;
                }
                catch
                {
                    await Cleanup();
                    tryCount++;
                }
            }
        }

        private static void ProductCardZoomAndOpen(int index)
        {
            Driver.Hover("product-image-wrapper-0").ClickId($"fancybox-button-{index}");
            Thread.Sleep(250);
            Driver.FindElementByClassName("fancybox-close").Click();
            Driver.ClickId($"product-details-link-{index}");
        }

        private static void AddProduct(ProductStub stub)
        {
            Driver.ClickId("create-new-product-link");
            Driver.FindElementById("ProductName").SendKeys(stub.Name);
            Driver.FindElementById("LinkName").SendKeys(stub.Slug);
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
            Driver.FindElementById("adminLogin").Click();
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
            Thread.Sleep(5);
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
            Driver.Navigate().GoToUrl(BaseUrl);
            Driver.FindElementById("manage-account-header-link").Click();
            Driver.FindElementById("register-account-link").Click();
            Driver.FindElementById("Email").SendKeys(Email);
            Driver.FindElementByName("Password").SendKeys(Password);
            Driver.FindElementByName("ConfirmPassword").SendKeys(Password);
            Driver.FindElementById("register-account-submit-button").Click();
        }
    }
}
