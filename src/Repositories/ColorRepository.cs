using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly BtcEntities _db;

        public ColorRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task Create(Color color)
        {
            _db.Colors.Add(color);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Color>> Get() => await _db.Colors.ToListAsync();
    }
}
