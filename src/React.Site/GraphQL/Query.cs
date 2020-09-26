using System.Linq;
using Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace React.Site.GraphQL
{
    public class Query
    {
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories([Service] BtcEntities db) => db.Categories;

        public IQueryable<Product> GetProducts([Service] BtcEntities db) => db.Products.Include(x=>x.Image);

        public PublicSiteProfile GetSiteProfile([Service] BtcEntities db) => db.PublicSiteProfiles.OrderByDescending(x=>x.Id).FirstOrDefault();
    }
}
