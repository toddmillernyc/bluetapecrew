using BlueTapeCrew.EndToEndTests.Extensions;
using BlueTapeCrew.EndToEndTests.Models;
using OpenQA.Selenium;
using Xunit;

namespace BlueTapeCrew.EndToEndTests.Tests
{
    [Collection("EndToEnd")]
    public class AccountTests
    {
        private readonly EndToEndFixture _fixture;
        private readonly IWebDriver _driver;

        public AccountTests(EndToEndFixture fixture)
        {
            _fixture = fixture;
            _driver = fixture.Driver;
        }

        [Fact]
        public void Register_New_User()
        {
            //arrange
            var user = new User();

            //act
            _fixture.RegisterNewUserAndOpenAccountPage(user.Email, user.Password);
            var actualEmail = _driver.Value("User_Email");

            //assert
            Assert.Equal(user.Email, actualEmail);

            //cleanup
            _driver.ClickId("logoff");
            _fixture.RemoveAllIntegrationTestUsers();
        }
    }
}
