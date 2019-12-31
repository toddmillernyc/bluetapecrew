using AutoMapper;
using Moq;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace Btc.Tests
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
        //public Mock<IUserService> UserService = new Mock<IUserService>();
        //public Mock<ISessionService> SessionService = new Mock<ISessionService>();
        public Mock<IMapper> Mapper = new Mock<IMapper>();
        public Mock<IGuestUserRepository> GuestUserRepository = new Mock<IGuestUserRepository>();

        //public Mock<UserManager<TUser>> GetMockUserManager<TUser>(List<TUser> ls) where TUser : class
        //{
        //    var store = new Mock<IUserStore<TUser>>();
        //    var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        //    mgr.Object.UserValidators.Add(new UserValidator<TUser>());
        //    mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        //    mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        //    mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
        //    mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
        //    return mgr;
        //}
    }
}
