using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class InvoiceServiceTests
    {
        private readonly IntegrationTextFixture _fixture;
        public InvoiceServiceTests(IntegrationTextFixture fixture) { _fixture = fixture; }

        [Fact]
        public async Task Create_GivenASessionId_CreatesAnInvoice()
        {
            //arrange
            var sessionId = Guid.NewGuid().ToString();

            //act
            var actual = await _fixture.InvoiceService.Create(sessionId);

            //assert
            Assert.True(actual.Id > 0);

            //teardown
            _fixture.Teardown(actual);
        }
    }
}
