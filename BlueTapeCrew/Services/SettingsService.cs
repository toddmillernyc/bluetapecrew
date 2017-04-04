using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class SettingsService : ISettingsService
    {
        public async Task<SiteSetting> Get()
        {
            using (var db = new BtcEntities())
            {
                return await db.SiteSettings.FirstOrDefaultAsync();
            }
        }

        public async Task Update(SiteSetting settings)
        {
            using (var db = new BtcEntities())
            {
                db.Entry(settings).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}