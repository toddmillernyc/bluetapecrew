using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Repositories
{
    public interface ISendgridSettingsRepository
    {
        Task<SendgridSetting> Get();
        Task<SendgridSetting> Save(SendgridSetting sendgridSettings);
        Task<SendgridSetting> Create(SendgridSetting sendgridSetting);
        Task Delete(int id);
    }
}
