using System;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Paypal;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Services;
using BlueTapeCrew.Tests.Stubs;

namespace BlueTapeCrew.Tests
{
    public class IntegrationTestBase
    {
        //constants
        protected const string PaypalApi = "https://api.sandbox.paypal.com/v1/";

        protected Uri ProductionCheckoutUri => ConfigurationStubs.ProductionCheckoutUri;

        //repositories
        protected IAccessTokenRepository AccessTokenRepository => new AccessTokenRepository();
        protected IInvoiceRepository InvoiceRepository => new InvoiceRepository();
        protected ISettingsRepository SettingsRepository => new SettingsRepository();

        //services
        protected IInvoiceService InvoiceService;
        protected IPaypalService PaypalService => new PaypalService();
        

        protected SiteSetting Settings;

        //stubs
        public IntegrationTestBase()
        {
            Settings = SettingsRepository.Get().Result;
        }

        protected InvoiceService GetInvoiceService()
        {
            return new InvoiceService(InvoiceRepository);
        }
    }
}
