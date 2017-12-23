using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> Create(string sessionId);
        Task Delete(int id);
    }
}
