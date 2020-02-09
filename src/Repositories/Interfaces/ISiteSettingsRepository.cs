using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface ISiteSettingsRepository
    {
        Task<IEnumerable<SiteSetting>> GetAll();
        Task<SiteSetting> Set(SiteSetting siteSetting);
        Task Create(SiteSetting siteSetting);
    }
}
