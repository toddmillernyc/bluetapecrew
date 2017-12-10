using System.Linq;
using System.Web.Http;
using BlueTapeCrew.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SiteSettingsController : ApiController
    {
        public SiteSetting Get()
        {
            using (var db = new BtcEntities())
            {
                return db.SiteSettings.FirstOrDefault();
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task Post([FromBody]SiteSetting settings)
        {
            using (var db = new BtcEntities())
            {
                var model = await db.SiteSettings.FirstOrDefaultAsync();

                db.SiteSettings.Add(settings);
                await db.SaveChangesAsync();

                db.SiteSettings.Remove(model);
                await db.SaveChangesAsync();
            }
        }
    }
}
