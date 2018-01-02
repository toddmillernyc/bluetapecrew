using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> Create(string sessionId);
        Task Delete(int id);
    }
}
