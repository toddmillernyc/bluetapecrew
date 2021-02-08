using System;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class MenuService : IMenuService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;

        public MenuService(ICategoryRepository categoryRepository,
            IImageRepository imageRepository)
        {
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
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
            foreach (var category in categories)
            {
                var imageId = category.ProductCategories.FirstOrDefault()?.Product.ImageId ?? 0;
                var image = await _imageRepository.Find(imageId);
                model.Add(new MobileCategory
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImageData = Convert.ToBase64String(image.ImageData)

                }); 
            }
            return model;
        }
    }
}
