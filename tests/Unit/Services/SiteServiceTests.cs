using AutoMapper;
using Moq;
using Repositories.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests
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
                var siteProfileRepository = new Mock<ISiteProfileRepository>();
                var mapper = new Mock<IMapper>();
                var sut = new SiteSettingsService(siteSettingsRepository.Object, mapper.Object, siteProfileRepository.Object);
                
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
