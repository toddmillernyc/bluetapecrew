using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISiteSettingsService _settings;
        

        public ViewModelService(
            ICategoryRepository categoryRepository,
            ISiteSettingsService settings
            )
        {
            _categoryRepository = categoryRepository;
            _settings = settings;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var settings = await _settings.Get();
            var catalog = new List<CatalogModel>();
            var categories = await _categoryRepository.GetAllPublishedWithProductsAndStyles();

            foreach (var category in categories)
            {
                var catalogModel = new CatalogModel(category.CategoryName);
                var categoryProducts = category.ProductCategories.Select(x => x.Product).OrderBy(x => x.ProductName);
                foreach (var product in categoryProducts)
                {
                    catalogModel.Products.Add(new ProductsAzViewModel
                    {
                        Id = product.Id,
                        Name = product.ProductName,
                        LinkName = product.LinkName,
                        Price = $"{product.Styles?.FirstOrDefault()?.Price:n2}",
                        ImgSource = "images/" + product.LinkName + ".jpg"
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
            var categories = await _categoryRepository.GetAllPublishedWithProducts();
            return categories.Select(item => new MenuViewModel
            {
                Id = item.Id,
                MenuName = item.CategoryName,
                Items = item.ProductCategories.Select(x=>x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                {
                    LinkName = product.LinkName,
                    ItemName = product.ProductName
                })
            });
        }

        public async Task<LayoutViewModel> GetLayoutViewModel()
        {
            var settings = await _settings.Get();
            if(settings == null) return new LayoutViewModel();
            var categories = await _categoryRepository.GetAllPublishedWithProducts();

            return new LayoutViewModel
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
                    MenuName = item.CategoryName,
                    Items = item.ProductCategories.Select(x=>x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                    {
                        LinkName = product.LinkName,
                        ItemName = product.ProductName
                    })
                }).ToList(),
                ShowSubscibeForm = !string.IsNullOrEmpty(settings.MailChimpListId) && !string.IsNullOrEmpty(settings.MailChimpApiKey)
            };
        }
    }
}