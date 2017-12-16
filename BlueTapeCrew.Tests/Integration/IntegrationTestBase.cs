using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Services;

namespace BlueTapeCrew.Tests.Integration
{
    public class IntegrationTestBase
    {
        public const string PaypalApi = "https://api.sandbox.paypal.com/v1/";
        public string PaypalTokenEndpoint = $"{PaypalApi}oauth2/token";

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
