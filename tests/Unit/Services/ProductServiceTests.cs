using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Repositories.Interfaces;
using Services;
using Services.Models;
using Xunit;

namespace Unit.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task Test()
        {
            //arrange
            const int expectedProductId = 1;
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Product, Entities.Product>(It.IsAny<Product>())).Returns(new Entities.Product
            {
                Id = expectedProductId
            });
            var sut = new ProductService(
                new Mock<IProductRepository>().Object,
                new Mock<IStyleRepository>().Object,
                new Mock<IReviewRepository>().Object,
                new Mock<IProductImageRepository>().Object,
                mockMapper.Object
                );

            //act
            var actualProduct = new Product { Id = 0 };
            await sut.Create(actualProduct);

            //assert
            Assert.Equal(expectedProductId, actualProduct.Id);
        }
    }
}
