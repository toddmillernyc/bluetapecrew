using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
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