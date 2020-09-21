using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class SiteProfileRepository : ISiteProfileRepository
    {
        private readonly BtcEntities _db;

        public SiteProfileRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<PublicSiteProfile> Get()
        {
            var entity = await _db.PublicSiteProfiles.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task Set(PublicSiteProfile entity)
        {
            _db.PublicSiteProfiles.Add(entity);
            await _db.SaveChangesAsync();
        }
    }
}
