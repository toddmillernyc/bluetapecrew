using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public  interface ISendgridSettingsService
    {
        Task<SendgridSetting> Get();
        Task<SendgridSetting> Set(SendgridSetting sendgridSetting);
        Task Delete(int id);
    }
}
