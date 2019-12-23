using BlueTapeCrew.Controllers;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Xunit;

namespace Btc.Tests.Unit
{
    [Collection("UnitTest")]
    public class CheckoutControllerTests
    {
        private readonly UnitTestFixture _fixture;

        public CheckoutControllerTests(UnitTestFixture fixture)
        {
            _fixture = fixture;
        }

        private CheckoutController GetSut()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "a" },
                new ApplicationUser { Id = "b" }
            };
            var userManager = _fixture.GetMockUserManager(users);
            return new CheckoutController(
                    _fixture.CartService.Object,
                    _fixture.CheckoutService.Object,
                    _fixture.EmailService.Object,
                    _fixture.OrderService.Object,
                    _fixture.SiteSettingsService.Object,
                    _fixture.UserService.Object,
                    userManager.Object,
                    _fixture.SessionService.Object
            );
        }

        [Fact]
        public async Task GetIndex_ReturnsEmptyCartView_IfCartIsEmpty()
        {
            //arrange
            _fixture.CartService
                    .Setup(x => x.GetCartViewModel(null))
                    .ReturnsAsync(new CartViewModel(new List<CartView>(), new CartTotals()));
            var sut = GetSut();

            //act
            var response = await sut.Index();

            //assert
            Assert.Equal("EmptyCart", response.ViewName);
        }
    }
}