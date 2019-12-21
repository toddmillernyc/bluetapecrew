using BlueTapeCrew.Data;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class ViewModelService : IViewModelService, IDisposable
    {
        private readonly BtcEntities _db;

        public ViewModelService(BtcEntities db)
        {
            _db = db;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var settings = await _db.SiteSettings.FirstOrDefaultAsync();
            var catalog = new List<CatalogModel>();
            var categories = await _db.Categories
                                        .Include(category => category.ProductCategories)
                                        .ThenInclude(productCategory => productCategory.Product)
                                        .ThenInclude(product => product.Styles)
                                        .OrderByDescending(category => category.ProductCategories.Count)
                                        .ToListAsync();

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
                Items = item.ProductCategories.Select(x=>x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
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
                    Items = item.ProductCategories.Select(x=>x.Product).OrderBy(x => x.ProductName).Select(product => new MenuItemViewModel
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