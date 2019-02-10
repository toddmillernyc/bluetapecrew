using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> Create(Invoice invoice);
        Task Delete(int id);
    }
}
