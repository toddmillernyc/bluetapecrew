using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;

namespace BlueTapeCrew.Services
{
    public class NewSiteService : INewSiteService
    {
        private readonly ISiteSettingsService _settings;

        public NewSiteService(ISiteSettingsService settings)
        {
            _settings = settings;
        }

        public void Setup()
        {
            _settings.Set(new SiteSetting
            {
                
            });
        }
    }
}
