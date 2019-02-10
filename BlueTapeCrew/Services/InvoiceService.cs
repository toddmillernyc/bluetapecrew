using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice> Create(string sessionId)
        {
            return await _invoiceRepository.Create(new Invoice(sessionId));
        }

        public async Task Delete(int id)
        {
            await _invoiceRepository.Delete(id);
        }
    }
}
