using BlueTapeCrew.Data;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
{
    public class CategoryProductsRepository : ICategoryProductsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public CategoryProductsRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await _db.Categories.Include(x => x.ProductCategories)
                .ThenInclude(x=>x.Product)
                .ThenInclude(x=>x.ProductCategories)
                .ToListAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}