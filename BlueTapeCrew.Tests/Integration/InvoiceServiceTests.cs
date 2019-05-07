using System;
using System.Threading.Tasks;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class InvoiceServiceTests
    {
        private readonly IntegrationTextFixture _fixture;
        public InvoiceServiceTests(IntegrationTextFixture fixture) { _fixture = fixture; }

        [Fact(Skip = "Skipping Integration Tests while setting up azure pipeline")]
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
