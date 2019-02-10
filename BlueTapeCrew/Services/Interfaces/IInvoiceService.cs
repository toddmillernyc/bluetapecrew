using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<Invoice> Create(string sessionId);
        Task Delete(int id);
    }
}
