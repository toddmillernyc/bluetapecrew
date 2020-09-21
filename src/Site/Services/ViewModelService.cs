using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Services.Interfaces;
using Services.Models;
using Site.Models;
using Site.ViewModels;

namespace Site.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly ICategoryService _categoryService;
        private readonly ISiteSettingsService _settings;
        private readonly IStyleService _styleService;
        private readonly IProductService _productService;
        private readonly FeatureToggles _featureToggles;

        public ViewModelService(
            ICategoryService categoryService,
            ISiteSettingsService settings,
            IStyleService styleService,
            IProductService productService,
            IOptions<FeatureToggles> featureToggles)
        {
            _settings = settings;
            _styleService = styleService;
            _productService = productService;
            _categoryService = categoryService;
            _featureToggles = featureToggles.Value;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var siteProfile = await _settings.GetSiteProfile();
            var catalog = new List<CatalogModel>();
            var categories = await _categoryService.GetAllPublishedWithProductsAndStyles();

            foreach (var category in categories)
            {
                var catalogModel = new CatalogModel(category.Name, category.Position);
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
                SiteTitle = siteProfile?.SiteTitle ?? "NEW SITE",
                Description = siteProfile?.Description ?? "NEW SITE DESCRIPTION"
            });
        }

        public async Task<IEnumerable<MenuViewModel>> GetSidebarViewModel()
        {
            var categories = await _categoryService.GetAllPublishedWithProducts();
            return categories.Select(item => new MenuViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Position = item.Position,
                Items = item.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                {
                    LinkName = product.Slug,
                    ItemName = product.ProductName
                })
            });
        }

        public async Task<LayoutViewModel> GetLayoutViewModel()
        {
            var profile = await _settings.GetSiteProfile();
            if (profile == null) return new LayoutViewModel();
            var categories = await _categoryService.GetAllPublishedWithProducts();

            return new LayoutViewModel
            {
                ContactEmail = profile.ContactEmailAddress,
                ContactPhone = profile.ContactPhoneNumber,
                Description = profile.Description,
                Keywords = profile.Keywords,
                AboutUs = profile.AboutUs,
                SiteTitle = profile.SiteTitle,
                TwitterHandle = $"https://twitter.com/{profile.TwitterUrl}?ref_src=twsrc%5Etfw",
                FaceBookUrl = profile.FaceBookUrl,
                LinkedInUrl = profile.LinkedInUrl,
                CopyrightLinktext = profile.CopyrightLinktext,
                CopyrightText = profile.CopyrightText,
                CopyrightUrl = profile.CopyrightUrl,
                Menu = categories.Select(item => new MenuViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Position = item.Position,
                    Items = item.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                    {
                        LinkName = product.Slug,
                        ItemName = product.ProductName
                    })
                }).ToList(),
                ShowSubscibeForm = _featureToggles.SubscribeForm
            };
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