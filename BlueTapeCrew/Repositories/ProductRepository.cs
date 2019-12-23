using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public ProductRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<Product> FindBySlug(string slug)
        {
            var product = await _db.Products
                                    .Include( p => p.Styles)
                                    .ThenInclude(s => s.Color)
                                    .Include(p => p.Styles)
                                    .ThenInclude(s => s.Size)
                                    .Include(p => p.ProductCategories)
                                    .ThenInclude(pc => pc.Category)
                                    .Include(p => p.Image)
                                    .Include(p => p.ProductImages)
                                    .ThenInclude(pi => pi.Image)
                                    .Include(p => p.Reviews)
                                    .FirstOrDefaultAsync(p => p.LinkName == slug);
            return product;
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}