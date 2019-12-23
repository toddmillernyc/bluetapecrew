using BlueTapeCrew.Data;
using BlueTapeCrew.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Entities;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace BlueTapeCrew.Services
{
    public class SettingsService : ISiteSettingsService, IDisposable
    {
        private readonly BtcEntities _db;

        public SettingsService(BtcEntities db)
        {
            _db = db;
        }

        public async Task<SiteSetting> Get()
        {
            return await _db.SiteSettings.FirstOrDefaultAsync();
        }

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            _db.SiteSettings.RemoveRange(_db.SiteSettings);
            _db.SiteSettings.Add(siteSetting);
            await _db.SaveChangesAsync();
            return siteSetting;
        }

        public async Task Update(SiteSetting settings)
        {
            _db.Entry(settings).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}