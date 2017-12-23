using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Repositories
{
    public interface ISettingsRepository
    {
        Task<SiteSetting> Get();
    }
}
