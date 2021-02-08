using System.Threading.Tasks;
using Moq;
using Services.Interfaces;
using Site.Controllers.Admin;
using Xunit;

namespace Unit.Controllers
{
    public class ApiControllerTests
    {
        [Fact]
        public async Task GivenARequest_Get_DoesNotThrowAnException()
        {
            //arrange
            var siteSettingsService = new Mock<ISiteSettingsService>();
            var sut = new SiteSettingsController(siteSettingsService.Object);

            try
            {
                //act
                await sut.Get();

                //assert
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }
    }
}
