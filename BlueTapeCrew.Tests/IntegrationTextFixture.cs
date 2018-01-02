using System;
using System.Collections.Concurrent;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models.Entities;
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
        public static ISiteSettingsRepository SiteSettingsRepository => new SiteSettingsRepository();

        //services
        public IInvoiceService InvoiceService => new InvoiceService(InvoiceRepository);
        public IEmailService EmailService => new Services.EmailService();
        public IPaypalService PaypalService => new PaypalService();
        public static ISiteSettingsService SiteSettingsService => new SiteSettingsService(SiteSettingsRepository);

        public IntegrationTextFixture()
        {
            SiteSettings = SiteSettingsService.Get().Result;
        }

        public void Teardown(object entity)
        {
            _objectsToDelete.Add(entity);
        }

        public async void Dispose()
        {
            foreach (var entity in _objectsToDelete)
            {
                switch (entity)
                {
                    case Invoice invoice:
                        await InvoiceService.Delete(invoice.Id);
                        break;
                }
            }
        }



    }
}
