using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

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
            if (invoice == null) return;
            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}