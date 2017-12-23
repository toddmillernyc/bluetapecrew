using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;

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
