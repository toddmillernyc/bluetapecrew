using System;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Paypal;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Services;

namespace BlueTapeCrew.Tests.Integration
{
    public class IntegrationTestBase
    {
        protected const string PaypalApi = "https://api.sandbox.paypal.com/v1/";

        protected IAccessTokenRepository AccessTokenRepository;
        protected ISettingsRepository SettingsRepository;
        protected IInvoiceRepository InvoiceRepository;
        protected SiteSetting Settings;

        protected Uri RequestUri => new Uri("https://bluetapecrew.come/checkout/", UriKind.Absolute);

        public IntegrationTestBase()
        {
            AccessTokenRepository = new AccessTokenRepository();
            InvoiceRepository = new InvoiceRepository();
            SettingsRepository = new SettingsRepository();
            Settings = SettingsRepository.Get().Result;
        }

        protected PaypalService GetPaypalService()
        {
            return new PaypalService();
        }

        protected InvoiceService GetInvoiceService()
        {
            return new InvoiceService(InvoiceRepository);
        }
    }
}
