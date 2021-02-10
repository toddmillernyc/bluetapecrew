using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace Services
{
    public class MenuService : IMenuService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public MenuService(ICategoryRepository categoryRepository,
            IImageRepository imageRepository, IImageService imageService)
        {
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<MenuCategory>> Get()
        {
            var categories = await _categoryRepository.GetAllWithProducts();
            return categories
                .OrderBy(X => X.Position)
                .Select(category =>
                    new MenuCategory
                    {
                        Name = category.Name,
                        IsPublished = category.Published,
                        Products = category.ProductCategories
                            .Select(x => x.Product)
                            .OrderBy(x => x.Slug)
                            .ToDictionary(x => x.Slug, x => x.ProductName)
                    });
        }

        public async Task<IEnumerable<MobileCategory>> GetMobileMenu()
        {
            var model = new List<MobileCategory>();
            var categories = await _categoryRepository.GetAllPublishedWithProducts();
            foreach (var category in categories.OrderBy(x=>x.Position))
            {
                var imageId = category.ProductCategories.FirstOrDefault()?.Product.ImageId ?? 0;
                var image = await _imageRepository.Find(imageId);
                var resizedImage = await _imageService.ResizeImage(image.ImageData, 96, 128, ImageFormat.Jpeg);
                model.Add(new MobileCategory
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImageData = resizedImage,
                }); 
            }
            return model;
        }
    }
}
