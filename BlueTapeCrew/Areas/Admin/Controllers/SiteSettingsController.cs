using System.Linq;
using System.Web.Http;
using BlueTapeCrew.Models;

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
        public void Post([FromBody]SiteSetting settings)
        {
            using (var db = new BtcEntities())
            {
                var model = db.SiteSettings.FirstOrDefault();
                model.SiteTitle = settings.SiteTitle;
                model.Description = settings.Description;
                model.Keywords = settings.Keywords;
                model.AboutUs = settings.AboutUs;
                model.TwitterWidgetId = settings.TwitterWidgetId;
                db.SaveChanges();
            }
        }
    }
}
