using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using React.Site.Identity;
using React.Site.Models;

namespace React.Site.GraphQL
{
    public class Query
    {
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories([Service] BtcEntities db) => db.Categories;

        public async Task<ImageData> GetImageData(int id, [Service] BtcEntities db)
        {
            var imageBytes = (await db.Images.FindAsync(id)).ImageData;
            var base64ImageString = Convert.ToBase64String(imageBytes);
            return new ImageData {Id = id, Src = base64ImageString};
        }

        public IQueryable<Product> GetProducts([Service] BtcEntities db) => db.Products;

        public async Task<PublicSiteProfile> GetSiteProfile([Service] BtcEntities db) => await db.PublicSiteProfiles.OrderByDescending(x=>x.Id).FirstOrDefaultAsync();

        public async Task<ApplicationUser> GetUserProfile([Service] UserManager<ApplicationUser> userManager, string emailAddress) 
            => await userManager.FindByEmailAsync(emailAddress);
    }
}
