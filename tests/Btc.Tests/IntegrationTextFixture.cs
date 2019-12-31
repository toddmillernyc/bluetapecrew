using BlueTapeCrew;
using Btc.Tests.Stubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using Entities;
using Xunit;

namespace Btc.Tests
{
    [CollectionDefinition("IntegrationTest")]
    public class IntegrationTest : ICollectionFixture<IntegrationTextFixture> { }

    public class IntegrationTextFixture : IDisposable
    {
        //teardown objects
        private readonly ConcurrentBag<object> _objectsToDelete = new ConcurrentBag<object>();

        //fields
        public Uri ProductionCheckoutUri => ConfigurationStubs.ProductionCheckoutUri;

        public ServiceProvider Services;

        //services
        public T Resolve<T>()
        {
            var service = Services.GetRequiredService<T>();
            return service;
        }

        public IntegrationTextFixture()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.integrationtest.json")
                .Build();

            var serviceCollection = new ServiceCollection()
                .AddDbContext<BtcEntities>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection")))
                .AddEntityFrameworkSqlServer();

            Startup.RegisterRepositoryTypes(serviceCollection);
            Startup.RegisterServiceTypes(serviceCollection);

            Services = serviceCollection.BuildServiceProvider();
        }



        public void Teardown(object entity)
        {
            _objectsToDelete.Add(entity);
        }

        public async void Dispose()
        {

        }



    }
}
