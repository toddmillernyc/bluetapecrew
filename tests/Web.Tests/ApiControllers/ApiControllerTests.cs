using Moq;
using Services.Interfaces;
using System.Threading.Tasks;
using Site.ApiControllers;
using Xunit;

namespace Web.Tests.ApiControllers
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
