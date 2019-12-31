using System.Threading.Tasks;
using BlueTapeCrew.Controllers;
using BlueTapeCrew.Services;
using Moq;
using Xunit;

namespace Web.Tests.Controllers
{
    public class UnitTest1
    {
        [Fact]
        public async Task GivenARequest_Index_DoesNotThrowAnException()
        {
            //arrange
            var viewModelService = new Mock<IViewModelService>();
            var sut = new HomeController(viewModelService.Object);


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
