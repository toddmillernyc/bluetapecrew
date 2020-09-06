using System.Threading.Tasks;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly BtcEntities _db;

        public ProductImageRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task Create(ProductImage productImage)
        {
            _db.ProductImages.Add(productImage);
            await _db.SaveChangesAsync();
        }
    }
}
