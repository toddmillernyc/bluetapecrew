using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Repositories
{
    public class SendgridSettingsRepository : ISendgridSettingsRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public SendgridSettingsRepository()
        {
            _db = new BtcEntities();
        }

        public async Task<SendgridSetting> Get()
        {
            return await _db.SendgridSettings.FirstOrDefaultAsync();
        }

        public async Task<SendgridSetting> Save(SendgridSetting sendgridSettings)
        {
            var entity = await _db.SendgridSettings.FindAsync(sendgridSettings.Id);
            if (entity == null) return null;
            _db.Entry(entity).CurrentValues.SetValues(sendgridSettings);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<SendgridSetting> Create(SendgridSetting sendgridSettings)
        {
            var entity = _db.SendgridSettings.Add(sendgridSettings);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var settings = await _db.SendgridSettings.FindAsync(id);
            if(settings == null) return;
            _db.SendgridSettings.Remove(settings);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}