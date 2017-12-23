using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Repositories
{
    public class SettingsRepository : ISettingsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public SettingsRepository()
        {
            _db = new BtcEntities();
        }

        public async Task<SiteSetting> Get()
        {
            return await _db.SiteSettings.FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}