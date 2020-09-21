using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Models;
using Xunit;

namespace Unit.Controllers
{
    [Collection("UnitTest")]
    public class CheckoutControllerTests
    {
        private readonly UnitTestFixture _fixture;

        public CheckoutControllerTests(UnitTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetIndex_ReturnsEmptyCartView_IfCartIsEmpty()
        {
            _fixture
                .CheckoutService
                .Setup(x => x.CreateCheckoutRequest(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new CheckoutRequest
                {
                    Cart = new CartViewModel(new List<CartView>(), new CartTotals() )
                });
            var sut = _fixture.GetCheckoutController();

            //act
            var response = await sut.Index();
            var viewResult = (ViewResult) response;

            //assert
            Assert.Equal("EmptyCart", viewResult.ViewName);
        }
    }
}