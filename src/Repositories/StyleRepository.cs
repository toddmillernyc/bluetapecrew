using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class StyleRepository : IStyleRepository
    {
        private readonly BtcEntities _db;

        public StyleRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StyleView>> GetByProductId(int id) 
            => await _db.StyleViews
                .Where(x => x.ProductId == id)
                .OrderBy(x => x.SizeOrder)
                .ThenBy(x => x.ColorText)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Style> Find(int id) => await _db.Styles.FindAsync(id);

        public async Task Delete(int id)
        {
            var style = await _db.Styles.FindAsync(id);
            var carts = await _db.Carts.Where(x => x.StyleId.Equals(id)).ToListAsync();
            _db.Carts.RemoveRange(carts);
            _db.Styles.Remove(style);
            await _db.SaveChangesAsync();
        }



        public async Task Create(Style style)
        {
            _db.Styles.Add(style);
            await _db.SaveChangesAsync();
        }
    }
}
