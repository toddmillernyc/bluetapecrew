using System.Threading.Tasks;
using Moq;
using Services;
using Services.Interfaces;
using Services.Models;
using Xunit;

namespace Unit.Services
{

    public class ShippingServiceTests
    {
        private readonly Mock<ISiteSettingsService> _siteSettingsService = new Mock<ISiteSettingsService>();

        public ShippingServiceTests()
        {
            _siteSettingsService.Setup(x => x.Get()).Returns(Task.FromResult(new SiteSetting
            {
                FreeShippingThreshold = 5.00m,
                FlatShippingRate = 19.99m
            }));
        }

        [Fact]
        public async Task Calculate_Returns_FreeShipping_If_EqualToShippingThreshold()
        {
            //arrange
            var sut = new ShippingService(_siteSettingsService.Object);

            //act
            var actual = await sut.Calculate(5.00m);

            //assert
            Assert.Equal(0.00m, actual);
        }

        [Fact]
        public async Task Calculate_Returns_FlatShipping_LessThanShippingThreshold()
        {
            //arrange
            var sut = new ShippingService(_siteSettingsService.Object);

            //act
            var actual = await sut.Calculate(4.99m);

            //assert
            Assert.Equal(19.99m, actual);
        }

        [Fact]
        public async Task Calculate_Returns_FreeShipping_GreaterThanShippingThreshold()
        {
            //arrange
            var sut = new ShippingService(_siteSettingsService.Object);

            //act
            var actual = await sut.Calculate(5.01m);

            //assert
            Assert.Equal(0.00m, actual);
        }

        [Fact]
        public async Task Calculate_Returns_FreeShipping_Given_ZeroSubtotal()
        {
            //arrange
            var sut = new ShippingService(_siteSettingsService.Object);

            //act
            var actual = await sut.Calculate(0.00m);

            //assert
            Assert.Equal(0.00m, actual);
        }
    }
}
