using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Interfaces
{
    public interface ISiteSettingsService
    {
        Task<SiteSetting> GetSettings();
    }
}
