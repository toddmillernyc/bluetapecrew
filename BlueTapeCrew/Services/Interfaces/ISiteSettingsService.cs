using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ISiteSettingsService
    {
        Task<SiteSetting> Get();
        Task<SiteSetting> Set(SiteSetting siteSetting);
    }
}
