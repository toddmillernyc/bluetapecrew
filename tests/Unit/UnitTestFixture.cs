using Moq;
using Services.Interfaces;
using Site.Services;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Site.Controllers;
using Site.Services.Interfaces;
using Xunit;

namespace Unit
{
    [CollectionDefinition("UnitTest")]
    public class UnitTest : ICollectionFixture<UnitTestFixture> { }

    public class UnitTestFixture
    {
        public Mock<IGuestUserService> GuestUserService = new Mock<IGuestUserService>();
        public Mock<ICartService> CartService = new Mock<ICartService>();
        public Mock<ICheckoutService> CheckoutService = new Mock<ICheckoutService>();
        public Mock<IOrderService> OrderService = new Mock<IOrderService>();
        public Mock<ISiteSettingsService> SiteSettingsService = new Mock<ISiteSettingsService>();
        public Mock<IUserService> UserService = new Mock<IUserService>();
        public Mock<ISessionService> SessionService = new Mock<ISessionService>();
        public Mock<IMapper> Mapper = new Mock<IMapper>();
        public readonly Mock<ICategoryService> CategoryService = new Mock<ICategoryService>();
        public readonly Mock<IFormFile> FormFile = new Mock<IFormFile>();
        public readonly Mock<IImageService> ImageService = new Mock<IImageService>();
        public readonly Mock<IProductService> ProductService = new Mock<IProductService>();
        public readonly Mock<IStyleService> StyleService = new Mock<IStyleService>();

        public UnitTestFixture()
        {
            ImageService.Setup(x => x.SaveImage(It.IsAny<SaveImageRequest>())).ReturnsAsync(new Image { Id = 1 });
        }

        public Mock<UserManager<TUser>> GetMockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            return mgr;
        }

        public CheckoutController GetCheckoutController()
        {
            var controller = new CheckoutController(
                CartService.Object,
                CheckoutService.Object,
                OrderService.Object,
                UserService.Object,
                SessionService.Object,
                Mapper.Object
            ) {ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}};
            controller.ControllerContext.HttpContext.Request.Path = "/unt-tests/checkout/";
            return controller;
        }
    }
}
