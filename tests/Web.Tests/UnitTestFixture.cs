using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Services.Interfaces;
using Services.Models;
using Xunit;

namespace Web.Tests
{
    [CollectionDefinition("UnitTest")]
    public class DatabaseCollection : ICollectionFixture<UnitTestFixture> { }

    public class UnitTestFixture
    {
        public readonly Mock<ICategoryService> CategoryService;
        public readonly Mock<IFormFile> FormFile;
        public readonly Mock<IImageService> ImageService;
        public readonly Mock<IMapper> Mapper;
        public readonly Mock<IProductService> ProductService;
        public readonly Mock<ISiteSettingsService> SiteSettingsService;
        public readonly Mock<IStyleService> StyleService;

        public UnitTestFixture()
        {
            CategoryService = new Mock<ICategoryService>();
            FormFile = new Mock<IFormFile>();
            ImageService = new Mock<IImageService>();
            Mapper = new Mock<IMapper>();
            ProductService = new Mock<IProductService>();
            SiteSettingsService = new Mock<ISiteSettingsService>();
            StyleService = new Mock<IStyleService>();
            Setup();
        }

        private void Setup()
        {
            ImageService.Setup(x => x.SaveImage(It.IsAny<SaveImageRequest>())).ReturnsAsync( new Image { Id = 1 } );
        }
    }
}
