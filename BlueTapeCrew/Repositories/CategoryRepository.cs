using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BlueTapeCrew.Repositories
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
        public Task<List<Category>> GetAllWithProducts() =>  _db.Categories
                                                                .Include(x => x.ProductCategories)
                                                                .ThenInclude(x => x.Product)
                                                                .ThenInclude(x => x.ProductCategories)
                                                                .ToListAsync();

        public Task<List<Category>> GetAllPublishedWithProducts() => _db.Categories
                                                                        .Where(x=>x.Published)
                                                                        .OrderBy(x=>x.CategoryName)
                                                                        .Include(x => x.ProductCategories)
                                                                        .ThenInclude(x => x.Product)
                                                                        .ThenInclude(x => x.ProductCategories)
                                                                        .ToListAsync();
    }
}
