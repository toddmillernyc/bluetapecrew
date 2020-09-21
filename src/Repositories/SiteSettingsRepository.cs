using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public class SiteSettingsRepository : ISiteSettingsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public SiteSettingsRepository(BtcEntities db)
        {
            _db = db;
        }

        public Task<SiteSetting> Get() => _db.SiteSettings.OrderByDescending(x=>x.Id).FirstOrDefaultAsync();

        public async Task Set(SiteSetting siteSetting)
        {
            _db.SiteSettings.Add(siteSetting);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            _db.SiteSettings.RemoveRange(_db.SiteSettings);
            await _db.SaveChangesAsync();
        }

        public async Task Create(SiteSetting siteSetting)
        {
            _db.SiteSettings.Add(siteSetting);
            await _db.SaveChangesAsync();
        }

        public void Dispose() => _db.Dispose();
    }
}