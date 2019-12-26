using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
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
