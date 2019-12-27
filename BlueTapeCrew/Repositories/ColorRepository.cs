using System.Collections.Generic;
using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlueTapeCrew.Repositories
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
