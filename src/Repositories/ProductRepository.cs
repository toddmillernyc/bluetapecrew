using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly BtcEntities _db;
        public ProductRepository(BtcEntities db) { _db = db; }

        public async Task<IEnumerable<Product>> GetProductsWithStylesAndImage(int take) => await _db.Products
                                                                                        .Include(x => x.Styles)
                                                                                        .Include(x => x.Image)
                                                                                        .Take(take)
                                                                                        .AsNoTracking()
                                                                                        .ToListAsync();

        public async Task Create(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> Find(int id) => await _db.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetAllIncludeAll() => await _db.Products
                                                                                .Include(p => p.Styles)
                                                                                .ThenInclude(s => s.Color)
                                                                                .Include(p => p.Styles)
                                                                                .ThenInclude(s => s.Size)
                                                                                .Include(p => p.ProductCategories)
                                                                                .ThenInclude(pc => pc.Category)
                                                                                .Include(p => p.Image)
                                                                                .Include(p => p.ProductImages)
                                                                                .ThenInclude(pi => pi.Image)
                                                                                .Include(p => p.Reviews)
                                                                                .AsNoTracking()
                                                                                .ToListAsync();

        public async Task Update(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<Product> FindIncludeAll(int id) => await _db.Products
                                                                                .Include(p => p.Styles)
                                                                                .ThenInclude(s => s.Color)
                                                                                .Include(p => p.Styles)
                                                                                .ThenInclude(s => s.Size)
                                                                                .Include(p => p.ProductCategories)
                                                                                .ThenInclude(pc => pc.Category)
                                                                                .Include(p => p.Image)
                                                                                .Include(p => p.ProductImages)
                                                                                .ThenInclude(pi => pi.Image)
                                                                                .Include(p => p.Reviews)
                                                                                .AsNoTracking()
                                                                                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Product>> GetByCategoryId(int categoryId)
        {
            var model = await _db.Products
                .Include(p => p.Styles)
                .Include(p => p.Image)
                .AsNoTracking()
                .Where(x => x.ProductCategories.Any(c => c.CategoryId == categoryId))
                .ToListAsync();
            return model;
        }


        public async Task<IEnumerable<Product>> GetAll() => await _db.Products.OrderBy(x => x.ProductName).AsNoTracking().ToListAsync();

        public Task<Product> FindBySlugIncludeAll(string slug) => _db.Products
                                                            .Include(p => p.Styles)
                                                            .ThenInclude(s => s.Color)
                                                            .Include(p => p.Styles)
                                                            .ThenInclude(s => s.Size)
                                                            .Include(p => p.ProductCategories)
                                                            .ThenInclude(pc => pc.Category)
                                                            .Include(p => p.Image)
                                                            .Include(p => p.ProductImages)
                                                            .ThenInclude(pi => pi.Image)
                                                            .Include(p => p.Reviews)
                                                            .AsNoTracking()
                                                            .FirstOrDefaultAsync(p => p.Slug == slug);

        public Task<Product> FindBySlugIncludeImage(string slug) => _db.Products
                                                                    .Include(p => p.Image)
                                                                    .FirstOrDefaultAsync(p => p.Slug == slug);

        public async Task<string> GetSlug(int productId) => (await _db.Products.FindAsync(productId)).Slug;

        public async Task Delete(int id)
        {
            var product =
                await _db.Products
                    .Include(p => p.Styles)
                    .ThenInclude(s => s.Color)
                    .Include(p => p.Styles)
                    .ThenInclude(s => s.Size)
                    .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                    .Include(p => p.Image)
                    .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                    .Include(p => p.Reviews)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

            var styles = await _db.Styles.Include(x => x.Carts).Where(style => style.ProductId == id).ToListAsync();

            //delete carts
            if (styles.Any() && styles.Any(x => x.Carts.Any()))
            {
                foreach (var style in styles)
                    _db.Carts.RemoveRange(style.Carts);
                await _db.SaveChangesAsync();
            }

            if (styles.Any())
            {
                _db.Styles.RemoveRange(styles);
                await _db.SaveChangesAsync();
            }

            if (product.ImageId > 0)
            {
                var image = await _db.Images.FindAsync(product.ImageId);
                _db.Images.Remove(image);
            }

            if (product.ProductImages.Any())
            {
                _db.RemoveRange(product.ProductImages);
                _db.Images.RemoveRange(product.ProductImages.Select(x => x.Image));
            }

            if (product.ProductCategories.Any()) _db.ProductCategories.RemoveRange(product.ProductCategories);
            if (product.CartImages.Any()) _db.CartImages.RemoveRange(product.CartImages);

            if (product.Reviews.Any()) _db.Reviews.RemoveRange(product.Reviews);


            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public void Dispose() => _db?.Dispose();
    }
}