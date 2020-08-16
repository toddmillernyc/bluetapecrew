using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using Site.Services;
using Xunit;

namespace Unit
{
    [CollectionDefinition("UnitTest")]
    public class DatabaseCollection : ICollectionFixture<UnitTestFixture> { }

    public class UnitTestFixture
    {
        public readonly Mock<ICategoryService> CategoryService;
        public readonly Mock<IFormFile> FormFile;
        public readonly Mock<IImageService> ImageService;
        public readonly Mock<IMapper> Mapper;
        public readonly Mock<IProductService> ProductService;
        public readonly Mock<ISiteSettingsService> SiteSettingsService;
        public readonly Mock<IStyleService> StyleService;
        public Mock<IGuestUserService> GuestUserService = new Mock<IGuestUserService>();
        public Mock<ICartService> CartService = new Mock<ICartService>();
        public Mock<ICheckoutService> CheckoutService = new Mock<ICheckoutService>();
        public Mock<IEmailService> EmailService = new Mock<IEmailService>();
        public Mock<IOrderService> OrderService = new Mock<IOrderService>();
        public Mock<IUserService> UserService = new Mock<IUserService>();
        public Mock<ISessionService> SessionService = new Mock<ISessionService>();
        public Mock<IGuestUserRepository> GuestUserRepository = new Mock<IGuestUserRepository>();

        public UnitTestFixture()
        {
            CategoryService = new Mock<ICategoryService>();
            FormFile = new Mock<IFormFile>();
            ImageService = new Mock<IImageService>();
            Mapper = new Mock<IMapper>();
            ProductService = new Mock<IProductService>();
            SiteSettingsService = new Mock<ISiteSettingsService>();
            StyleService = new Mock<IStyleService>();
            Setup();
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

        private void Setup()
        {
            ImageService.Setup(x => x.SaveImage(It.IsAny<SaveImageRequest>())).ReturnsAsync( new Image { Id = 1 } );
        }
    }
}
