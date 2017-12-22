using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Interfaces
{
    public interface IInvoiceService
    {
        Task<Invoice> Create(string sessionId);
        Task Delete(int id);
    }
}
