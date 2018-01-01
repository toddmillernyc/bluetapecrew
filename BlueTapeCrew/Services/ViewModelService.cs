using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Services
{
    public class ViewModelService : IViewModelService, IDisposable
    {
        private readonly BtcEntities _db = new BtcEntities();

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var settings = await _db.SiteSettings.FirstOrDefaultAsync();
            var catalog = new List<CatalogModel>();

            foreach (var category in _db.Categories.OrderByDescending(x => x.Products.Count))
            {
                var catalogModel = new CatalogModel
                {
                    CategoryName = category.CategoryName,
                    Products = new List<ProductsAzViewModel>()
                };
                foreach (var item in category.Products.OrderBy(x => x.ProductName))
                {
                    catalogModel.Products.Add(new ProductsAzViewModel
                    {
                        Id = item.Id,
                        Name = item.ProductName,
                        LinkName = item.LinkName,
                        // ReSharper disable once PossibleNullReferenceException
                        Price = $"{item.Styles.FirstOrDefault().Price:n2}",
                        ImgSource = "images/" + item.LinkName + ".jpg"
                    });
                }
                catalog.Add(catalogModel);
            }
            return (new HomeViewModel
            {
                Catalog = catalog,
                SiteTitle = settings.SiteTitle,
                Description = settings.Description
            });
        }

        public async Task<IEnumerable<MenuViewModel>> GetSidebarViewModel()
        {
            return await _db.Categories.Where(x => x.Published).OrderBy(x => x.CategoryName).Select(item => new MenuViewModel
            {
                Id = item.Id,
                MenuName = item.CategoryName,
                Items = item.Products.OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                {
                    LinkName = product.LinkName,
                    ItemName = product.ProductName
                })
            }).ToListAsync();
        }

        public async Task<LayoutViewModel> GetLayoutViewModel()
        {
            var settings = await _db.SiteSettings.FirstOrDefaultAsync();
            return new LayoutViewModel
            {
                ContactEmail = settings.ContactEmailAddress,
                ContactPhone = settings.ContactPhoneNumber,
                Description = settings.Description,
                Keywords = settings.Keywords,
                AboutUs = settings.AboutUs,
                TwitterWidgetId = settings.TwitterWidgetId,
                SiteTitle = settings.SiteTitle,
                TwitterUrl = settings.TwitterUrl,
                FaceBookUrl = settings.FaceBookUrl,
                LinkedInUrl = settings.LinkedInUrl,
                CopyrightLinktext = settings.CopyrightLinktext,
                CopyrightText = settings.CopyrightText,
                CopyrightUrl = settings.CopyrightUrl,
                Menu = await _db.Categories.Where(x => x.Published).OrderBy(x => x.CategoryName).Select(item => new MenuViewModel
                {
                    Id = item.Id,
                    MenuName = item.CategoryName,
                    Items = item.Products.OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
                    {
                        LinkName = product.LinkName,
                        ItemName = product.ProductName
                    })
                }).ToListAsync()
            };
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}