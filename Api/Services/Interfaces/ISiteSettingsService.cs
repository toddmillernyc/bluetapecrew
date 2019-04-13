using Api.Models.Entities;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ISiteSettingsService
    {
        Task<SiteSetting> Get();
        Task<SiteSetting> Set(SiteSetting siteSetting);
    }
}
