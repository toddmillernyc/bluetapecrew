using System;
using EndToEndTests.Extensions;
using OpenQA.Selenium;
using Xunit;

namespace EndToEndTests.Tests
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
            var id = Guid.NewGuid().ToString().Substring(0,5);
            var expected = $"BTC-integration-test-user-{id}@mailinator.com";
            var password = "A" + Guid.NewGuid().ToString().Substring(5);

            //act
            _fixture.RegisterNewUserAndOpenAccountPage(expected, password);
            var actual = _driver.Value("User_Email");
            _driver.ClickId("logoff");

            //assert
            Assert.Equal(expected, actual);

            //cleanup
            _fixture.RemoveAllIntegrationTestUsers();
        }
    }
}
