using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> Create(Invoice invoice);
        Task Delete(int id);
    }
}
