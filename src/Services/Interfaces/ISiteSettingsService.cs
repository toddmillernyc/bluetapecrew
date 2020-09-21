using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ISiteSettingsService
    {
        Task<SiteSetting> Get();
        Task Set(SiteSetting siteSetting);
        Task<SiteProfile> GetSiteProfile();
        Task SetSiteProfile(SiteProfile siteProfile);
    }
}
