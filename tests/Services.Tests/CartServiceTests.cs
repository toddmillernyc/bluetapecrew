using Moq;
using Services.Models;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests
{
    public class CartServiceTests : ServiceUnitTestBase
    {
        [Fact]
        public async Task GivenItemNotInCart_AddOrUpdate_SetsCountToOne()
        {
            //arrange
            var actual = new Repositories.Entities.Cart { Count = 0 };
            Mapper.Setup(mapper => mapper.Map<Repositories.Entities.Cart>(It.IsAny<Cart>())).Returns(actual);
            var sut = new CartService(CartCalculatorService.Object, CartRepository.Object, Mapper.Object);

            //act
            await sut.AddOrUpdate(new Cart());

            //assert
            Assert.True(actual.Count == 1);
        }
    }
}
