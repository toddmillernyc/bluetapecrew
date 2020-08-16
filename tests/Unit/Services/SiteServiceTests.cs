using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Repositories.Interfaces;
using Services;
using Xunit;

namespace Unit.Services
{
    public class SiteServiceTests
    {
        [Fact]
        public async Task GivenInvocation_Get_DoesNotThrowAnException()
        {
            try
            {
                //arrange
                var siteSettingsRepository = new Mock<ISiteSettingsRepository>();
                var mapper = new Mock<IMapper>();
                var sut = new SiteSettingsService(siteSettingsRepository.Object, mapper.Object);
                
                //act
                await sut.Get();

                //assert
                Assert.True(true);
            }
            catch
            {
                Assert.False(true);
            }

        }
    }
}
