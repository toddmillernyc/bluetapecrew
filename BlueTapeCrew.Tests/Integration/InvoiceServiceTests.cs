using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    public class InvoiceServiceTests : IntegrationTestBase, IDisposable
    {
        private readonly List<Invoice> _invoicesToDelete = new List<Invoice>();
        private readonly IInvoiceService _sut;

        public InvoiceServiceTests()
        {
            _sut = new InvoiceService(InvoiceRepository);
        }

        [Fact]
        public async Task Create_GivenASessionId_CreatesAnInvoice()
        {
            //arrange
            var sut = GetInvoiceService();
            var sessionId = Guid.NewGuid().ToString();

            //act
            var actual = await sut.Create(sessionId);
             _invoicesToDelete.Add(actual);

            //assert
            Assert.True(actual.Id > 0);
        }

        public void Dispose()
        {
            foreach (var invoice in _invoicesToDelete)
                _sut.Delete(invoice.Id);
        }
    }
}
