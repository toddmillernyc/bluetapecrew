using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services
{
    public class SendgridSettingsService : ISendgridSettingsService
    {
        private readonly ISendgridSettingsRepository _sendgridSettingsRepository;

        public SendgridSettingsService(ISendgridSettingsRepository sendgridSettingsRepository)
        {
            _sendgridSettingsRepository = sendgridSettingsRepository;
        }

        public async Task<SendgridSetting> Get()
        {
            return await _sendgridSettingsRepository.Get();
        }

        public async Task<SendgridSetting> Set(SendgridSetting sendgridSetting)
        {
            if (sendgridSetting.Id > 0) return await _sendgridSettingsRepository.Save(sendgridSetting);
            return await _sendgridSettingsRepository.Create(sendgridSetting);
        }

        public async Task Delete(int id)
        {
            await _sendgridSettingsRepository.Delete(id);
        }
    }
}
