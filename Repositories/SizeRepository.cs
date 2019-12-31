using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly BtcEntities _db;

        public SizeRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Size>> Get() => await _db.Sizes.ToListAsync();

        public async Task Create(Size size)
        {
            _db.Sizes.Add(size);
            await _db.SaveChangesAsync();
        }
    }
}
