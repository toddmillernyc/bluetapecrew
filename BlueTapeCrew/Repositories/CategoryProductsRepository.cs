using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Repositories
{
    public class CategoryProductsRepository : ICategoryProductsRepository, IDisposable
    {
        private readonly BtcEntities _db = new BtcEntities();

        public async Task<IEnumerable<Category>> Get()
        {
            return await _db.Categories.Include(x => x.Products).ToListAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}