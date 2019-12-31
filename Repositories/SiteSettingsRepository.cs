using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class SiteSettingsRepository : ISiteSettingsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public SiteSettingsRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<SiteSetting> Get()
        {
            var entities = await _db.SiteSettings.ToListAsync();
            return entities?.FirstOrDefault();
        }

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            var entity = await _db.SiteSettings.FindAsync(siteSetting.Id);
            _db.Entry(entity).CurrentValues.SetValues(siteSetting);
            await _db.SaveChangesAsync();
            return entity;
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