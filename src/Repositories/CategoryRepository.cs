using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BtcEntities _db;

        public CategoryRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<Category> Find(int id) => await _db.Categories.FindAsync(id);

        public async Task Update(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll() => await _db.Categories.ToListAsync();
        public async Task Create(Category category)
        {
            category.Position = _db.Categories.Max(x => x.Position) + 1;
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public Task<List<Category>> GetAllWithProducts() =>  _db.Categories
                                                                .Include(x => x.ProductCategories)
                                                                .ThenInclude(x => x.Product)
                                                                .ThenInclude(x => x.ProductCategories)
                                                                .ToListAsync();

        public Task<List<Category>> GetAllPublishedWithProducts() => _db.Categories
                                                                        .Where(x=>x.Published)
                                                                        .OrderBy(x=>x.Name)
                                                                        .Include(x => x.ProductCategories)
                                                                        .ThenInclude(x => x.Product)
                                                                        .ThenInclude(x => x.ProductCategories)
                                                                        .ToListAsync();

        public async Task<IEnumerable<Category>> GetAllPublishedWithProductsAndStyles()
        {
            var categories = await _db.Categories
                .Where(x=>x.Published)
                .Include(category => category.ProductCategories)
                .ThenInclude(productCategory => productCategory.Product)
                .ThenInclude(product => product.Styles)
                .OrderBy(x=>x.Position)
                .ToListAsync();
            return categories;
        }

        public async Task Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            var products = category.ProductCategories.ToList();
            foreach (var product in products) category.ProductCategories.Remove(product);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }
    }
}
