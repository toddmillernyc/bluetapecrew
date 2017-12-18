using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Services;

namespace BlueTapeCrew.Tests.Integration
{
    public class IntegrationTestBase
    {
        protected const string PaypalApi = "https://api.sandbox.paypal.com/v1/";

        protected IAccessTokenRepository AccessTokenRepository;
        protected ISettingsRepository SettingsRepository;
        protected IWebService WebService;

        protected SiteSetting Settings;

        public IntegrationTestBase()
        {
            AccessTokenRepository = new AccessTokenRepository();
            SettingsRepository = new SettingsRepository();
            WebService = new WebService();

            Settings = SettingsRepository.Get().Result;
        }


    }
}
