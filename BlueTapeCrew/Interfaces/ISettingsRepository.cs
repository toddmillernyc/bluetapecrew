using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Interfaces
{
    public interface ISettingsRepository
    {
        Task<SiteSetting> Get();
    }
}