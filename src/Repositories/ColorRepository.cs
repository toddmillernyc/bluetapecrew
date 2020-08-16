using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Entities;
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
            await _db.Colors.AddAsync(color);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Color>> Get()
        {
            var coloEntities = await _db.Colors.ToListAsync();
            return coloEntities;
        }
    }
}
