using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        public async Task<SiteSetting> GetSettings()
        {
            using (var db = new BtcEntities())
            {
                return await db.SiteSettings.FirstOrDefaultAsync();
            }
        }
    }
}