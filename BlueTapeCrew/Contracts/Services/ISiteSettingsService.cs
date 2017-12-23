using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public interface ISiteSettingsService
    {
        Task<SiteSetting> GetSettings();
    }
}
