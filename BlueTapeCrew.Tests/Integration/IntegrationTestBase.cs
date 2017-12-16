using BlueTapeCrew.Models;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Services;
using BlueTapeCrew.Repositories;

namespace BlueTapeCrew.Tests
{
    public class IntegrationTestBase
    {
        public const string PaypalSandboxUrl = "https://api.sandbox.paypal.com/v1/oauth2/token";
 
        public IAccessTokenRepository AccessTokenRepository;
        public ISettingsRepository SettingsRepository;
        public IWebService WebService;

        public SiteSetting Settings;

        public IntegrationTestBase()
        {
            AccessTokenRepository = new AccessTokenRepository();
            SettingsRepository = new SettingsRepository();
            WebService = new WebService();

            Settings = SettingsRepository.Get().Result;
        }
    }
}
