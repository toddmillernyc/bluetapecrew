using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Repositories
{
    public class SiteSettingsRepository : ISiteSettingsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public SiteSettingsRepository()
        {
            _db = new BtcEntities();
        }

        public async Task<SiteSetting> Get()
        {
            return await _db.SiteSettings.FirstOrDefaultAsync();
        }

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            var entity = await _db.SiteSettings.FindAsync(siteSetting.Id);
            _db.Entry(entity).CurrentValues.SetValues(siteSetting);
            await _db.SaveChangesAsync();
            return entity;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}