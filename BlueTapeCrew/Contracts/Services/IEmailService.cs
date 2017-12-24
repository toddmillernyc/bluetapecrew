using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IEmailService
    {
        Task SendEmail(SmtpRequest request);
    }
}