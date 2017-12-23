using System;
using System.Collections.Concurrent;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using BlueTapeCrew.Paypal;
using BlueTapeCrew.Repositories;
using BlueTapeCrew.Services;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests
{
    [CollectionDefinition("IntegrationTest")]
    public class IntegrationTest : ICollectionFixture<IntegrationTextFixture> { }

    public class IntegrationTextFixture : IDisposable
    {
        //teardown objects
        private readonly ConcurrentBag<object> _objectsToDelete = new ConcurrentBag<object>();

        //fields
        public Uri ProductionCheckoutUri => ConfigurationStubs.ProductionCheckoutUri;

        public SiteSetting SiteSettings;

        //repositories
        public static IInvoiceRepository InvoiceRepository => new InvoiceRepository();
        public static ISendgridSettingsRepository SendgridSettingsRepository => new SendgridSettingsRepository();
        public static ISettingsRepository SiteSettingsRepository => new SettingsRepository(); 

        //services
        public IInvoiceService InvoiceService => new InvoiceService(InvoiceRepository);
        public IPaypalService PaypalService => new PaypalService();
        public ISendgridSettingsService SendgridSettingsService => new SendgridSettingsService(SendgridSettingsRepository);
        public static ISiteSettingsService SiteSettingsService => new SiteSettingsService(SendgridSettingsRepository, SiteSettingsRepository);


        public IntegrationTextFixture()
        {
            SiteSettings = SiteSettingsService.GetSettings().Result;
        }

        public void Teardown(object entity)
        {
            _objectsToDelete.Add(entity);
        }

        public void Dispose()
        {
            foreach (var entity in _objectsToDelete)
            {
                switch (entity)
                {

                    case SendgridSetting sendgridSetting:
                        SendgridSettingsService.Delete(sendgridSetting.Id);
                        break;

                    case Invoice invoice:
                        InvoiceService.Delete(invoice.Id);
                        break;
                }
            }
        }



    }
}
