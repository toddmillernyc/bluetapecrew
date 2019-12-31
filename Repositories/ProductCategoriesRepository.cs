using System.Threading.Tasks;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProductCategoriesRepository : IProductCategoriesRepository
    {
        private readonly BtcEntities _db;

        public ProductCategoriesRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task Create(ProductCategory productCategory)
        {
            _db.ProductCategories.Add(productCategory);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(ProductCategory productCategory)
        {
            _db.ProductCategories.Remove(productCategory);
            await _db.SaveChangesAsync();
        }
    }
}
