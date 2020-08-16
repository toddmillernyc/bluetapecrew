using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Services.Models;
using Site;
using Site.Controllers;
using Site.Identity;
using Unit.Fixtures;
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
                    _fixture.OrderService.Object,
                    _fixture.UserService.Object,
                    _fixture.SessionService.Object,
                    _fixture.Mapper.Object,
                    _fixture.HttpContextAccessor.Object
            );
        }

        [Fact]
        public async Task GetIndex_ReturnsEmptyCartView_IfCartIsEmpty()
        {
            //arrange
            _fixture.CartService.Setup(x => x.GetCartViewModel(null)).ReturnsAsync(new CartViewModel(new List<CartView>(), new CartTotals()));
            _fixture.HttpContextAccessor.Setup(x => x.HttpContext.Request.Path).Returns("/Checkout");
            _fixture.HttpContextAccessor.Setup(x => x.HttpContext.User.Identity.Name).Returns("UnitTestUser");
            _fixture.UserService.Setup(x => x.Find(It.IsAny<string>())).ReturnsAsync(new User());
            _fixture.SessionService.Setup(x => x.SessionId()).Returns(Guid.NewGuid().ToString());
            _fixture.CheckoutService.Setup(x =>
                        x.CreateCheckoutRequest(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(new CheckoutRequest
                        {
                            Cart = new CartViewModel()
                        });
            var sut = GetSut();

            //act
            var response = await sut.Index();
            var viewResult = (ViewResult) response;

            //assert
            Assert.Equal("EmptyCart", viewResult.ViewName);
        }
    }
}