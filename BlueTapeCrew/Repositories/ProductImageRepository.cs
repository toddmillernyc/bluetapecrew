using System.Threading.Tasks;
using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;

namespace BlueTapeCrew.Repositories
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
