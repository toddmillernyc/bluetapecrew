using System.Threading.Tasks;
using Moq;
using Services;
using Services.Models;
using Xunit;

namespace Unit.Services
{
    public class CartServiceTests : ServiceUnitTestBase
    {
        [Fact]
        public async Task GivenItemNotInCart_AddOrUpdate_SetsCountToOne()
        {
            //arrange
            var actual = new Entities.Cart { Count = 1};
            Mapper.Setup(mapper => mapper.Map<Entities.Cart>(It.IsAny<Cart>())).Returns(actual);
            var sut = new CartService(CartCalculatorService.Object, CartRepository.Object, Mapper.Object);

            //act
            await sut.AddOrUpdate(new Cart());

            //assert
            Assert.Equal(1, actual.Count);
        }
    }
}
