using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Identity;
using Site.Services;
using Unit.Fixtures;
using Xunit;

namespace Unit.Services
{
    [Collection("UnitTest")]
    public class UserServiceTests
    {
        private readonly UnitTestFixture _fixture;

        public UserServiceTests(UnitTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Find_GivenANullEmail_ReturnsNull()
        {
            //arrange
            var userManager = _fixture.GetMockUserManager(new List<ApplicationUser>());
            var sut = new UserService(userManager.Object, _fixture.Mapper.Object , _fixture.GuestUserService.Object);
            
            //act
            var result = await sut.Find(null);

            Assert.Null(result);
        }
    }
}
