using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface ISiteSettingsRepository
    {
        Task<SiteSetting> Get();
        Task<SiteSetting> Set(SiteSetting siteSetting);
    }
}
