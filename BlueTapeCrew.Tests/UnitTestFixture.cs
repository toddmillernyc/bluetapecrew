using BlueTapeCrew.Services.Interfaces;
using Moq;
using Xunit;

namespace BlueTapeCrew.Tests
{
    [CollectionDefinition("UnitTest")]
    public class UnitTest : ICollectionFixture<UnitTestFixture> { }


    public class UnitTestFixture
    {
        public Mock<ICartService> CartService = new Mock<ICartService>();
        public Mock<ICheckoutService> CheckoutService = new Mock<ICheckoutService>();
        public Mock<IEmailService> EmailService = new Mock<IEmailService>();
        public Mock<IOrderService> OrderService = new Mock<IOrderService>();
        public Mock<ISiteSettingsService> SiteSettingsService = new Mock<ISiteSettingsService>();
        public Mock<IUserService> UserService = new Mock<IUserService>();
    }
}
