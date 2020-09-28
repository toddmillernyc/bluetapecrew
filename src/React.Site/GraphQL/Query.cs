using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using HotChocolate;
using HotChocolate.Types;
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
            return new ImageData {Src = base64ImageString};
        }

        public IQueryable<Product> GetProducts([Service] BtcEntities db) => db.Products;

        public PublicSiteProfile GetSiteProfile([Service] BtcEntities db) => db.PublicSiteProfiles.OrderByDescending(x=>x.Id).FirstOrDefault();
    }
}
