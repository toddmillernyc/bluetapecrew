using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly ICategoryService _categoryService;
        private readonly ISiteSettingsService _settings;
        private readonly IStyleService _styleService;
        private readonly IProductService _productService;

        public ViewModelService(
            ICategoryService categoryService,
            ISiteSettingsService settings,
            IStyleService styleService,
            IProductService productService)
        {
            _settings = settings;
            _styleService = styleService;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var settings = await _settings.Get();
            var catalog = new List<CatalogModel>();
            var categories = await _categoryService.GetAllPublishedWithProductsAndStyles();

            foreach (var category in categories)
            {
                var catalogModel = new CatalogModel(category.Name);
                var categoryProducts = category.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName);
                foreach (var product in categoryProducts)
                {
                    catalogModel.Products.Add(new ProductsAzViewModel
                    {
                        Id = product.Id,
                        Name = product.ProductName,
                        Slug = product.Slug,
                        Price = $"{product.Styles?.FirstOrDefault()?.Price:n2}",
                        ImgSource = "images/" + product.Slug + ".jpg"
                    });
                }
                catalog.Add(catalogModel);
            }
            return (new HomeViewModel
            {
                Catalog = catalog,
                SiteTitle = settings?.SiteTitle ?? "NEW SITE",
                Description = settings?.Description ?? "NEW SITE DESCRIPTION"
            });
        }

        public async Task<IEnumerable<MenuViewModel>> GetSidebarViewModel()
        {
            var categories = await _categoryService.GetAllPublishedWithProducts();
            return categories.Select(item => new MenuViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Items = item.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                {
                    Slug = product.Slug,
                    ItemName = product.ProductName
                })
            });
        }

        public async Task<LayoutViewModel> GetLayoutViewModel()
        {
            var settings = await _settings.Get();
            if (settings == null) return new LayoutViewModel();
            var categories = await _categoryService.GetAllPublishedWithProducts();
            var viewModel = new LayoutViewModel
            {
                ContactEmail = settings.ContactEmailAddress,
                ContactPhone = settings.ContactPhoneNumber,
                Description = settings.Description,
                Keywords = settings.Keywords,
                AboutUs = settings.AboutUs,
                SiteTitle = settings.SiteTitle,
                TwitterHandle = $"https://twitter.com/{settings.TwitterUrl}?ref_src=twsrc%5Etfw",
                FaceBookUrl = settings.FaceBookUrl,
                LinkedInUrl = settings.LinkedInUrl,
                CopyrightLinktext = settings.CopyrightLinktext,
                CopyrightText = settings.CopyrightText,
                CopyrightUrl = settings.CopyrightUrl,
                Menu = categories.Select(item => new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Items = item.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                    {
                        Slug = product.Slug,
                        ItemName = product.ProductName
                    })
                }).ToList(),
                ShowSubscibeForm = !string.IsNullOrEmpty(settings.MailChimpListId) && !string.IsNullOrEmpty(settings.MailChimpApiKey)
            };
            return viewModel;
        }

        public async Task<ProductViewModel> GetProductViewModel(string name)
        {
            var product = await _productService.FindBySlugIncludeAll(name);
            var styleViews = await _styleService.GetByProductId(product.Id);
            var bestSellers = await _productService.GetProductsWithStylesAndImage(3);
            var categories = await _categoryService.GetAllPublishedWithProducts();
            return new ProductViewModel(product, styleViews, bestSellers, categories);
        }
    }
}