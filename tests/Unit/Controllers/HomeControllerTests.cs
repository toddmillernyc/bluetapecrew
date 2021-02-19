using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Site.Controllers;
using Site.Services.Interfaces;
using Xunit;

namespace Unit.Controllers
{
    public class UnitTest1
    {
        [Fact]
        public async Task GivenARequest_Index_DoesNotThrowAnException()
        {
            //arrange
            var viewModelService = new Mock<IViewModelService>();
            var logger = new Mock<ILogger<HomeController>>();
            var sut = new HomeController(viewModelService.Object, logger.Object);

            try
            {
                //act
                await sut.Index();

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
