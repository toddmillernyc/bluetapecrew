using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ISiteSettingsService
    {
        Task<SiteSetting> Get();
        Task<SiteSetting> Set(SiteSetting siteSetting);
    }
}
