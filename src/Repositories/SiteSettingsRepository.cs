using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class SiteSettingsRepository : ISiteSettingsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public SiteSettingsRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SiteSetting>> GetAll()
        {
            var siteSettings = await _db.SiteSettings.ToListAsync();
            return siteSettings;
        }

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            var entity = await _db.SiteSettings.FindAsync(siteSetting.Id);
            _db.Entry(entity).CurrentValues.SetValues(siteSetting);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Create(SiteSetting siteSetting)
        {
            _db.SiteSettings.Add(siteSetting);
            await _db.SaveChangesAsync();
        }

        public void Dispose() => _db.Dispose();
    }
}