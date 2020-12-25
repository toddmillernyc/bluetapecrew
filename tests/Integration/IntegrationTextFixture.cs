using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Extensions;
using Site.Mappings;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Stubs;

namespace Btc.Tests
{
    [CollectionDefinition("IntegrationTest")]
    public class IntegrationTest : ICollectionFixture<IntegrationTextFixture> { }

    public class IntegrationTextFixture
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

            var serviceMappings = serviceCollection.AddServiceLayer();
            var webMappings = Assembly.GetAssembly(typeof(WebMappings));
            serviceCollection.AddAutoMapper(webMappings, serviceMappings);
            Services = serviceCollection.BuildServiceProvider();
        }

        public void Teardown(object entity)
        {
            _objectsToDelete.Add(entity);
        }
    }
}
