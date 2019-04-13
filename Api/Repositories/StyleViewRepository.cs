using Api.Models;
using Api.Models.Entities;
using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class StyleViewRepository : IStyleViewRepository
    {
        private readonly BtcEntities _db;

        public StyleViewRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StyleView>> GetBy(int productId)
        {
            var styleViews = await _db.StyleViews.FromSql($"SELECT * FROM StyleView WHERE productId={productId}").ToListAsync();
            return styleViews;
        }
    }
}
