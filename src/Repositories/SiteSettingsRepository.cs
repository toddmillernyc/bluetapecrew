using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
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

        public Task<SiteSetting> Get() => _db.SiteSettings.FirstOrDefaultAsync();

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