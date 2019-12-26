﻿using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
{
    public class StyleRepository : IStyleRepository
    {
        private readonly BtcEntities _db;

        public StyleRepository(BtcEntities db)
        {
            _db = db;
        }

        public Task<List<StyleView>> GetByProductId(int id) 
            => _db.StyleViews
                .Where(x => x.ProductId == id)
                .OrderBy(x => x.SizeOrder)
                .ThenBy(x => x.ColorText)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Style> Find(int id) => await _db.Styles.FindAsync(id);
    }
}