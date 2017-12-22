using System;
using System.Threading.Tasks;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Repositories
{
    public class InvoiceRepository : IInvoiceRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public InvoiceRepository()
        {
            _db = new BtcEntities();
        }

        public async Task<Invoice> Create(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
            return invoice;
        }

        public async Task Delete(int id)
        {
            var invoice = await _db.Invoices.FindAsync(id);
            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}