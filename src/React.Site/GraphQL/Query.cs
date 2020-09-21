using System.Linq;
using Entities;
using HotChocolate;
using HotChocolate.Types;

namespace React.Site.GraphQL
{
    public class Query
    {
        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories([Service] BtcEntities db) => db.Categories;

        public IQueryable<PublicSiteProfile> GetSiteProfiles([Service] BtcEntities db) => db.PublicSiteProfiles;
    }
}
