using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(SmtpRequest request);
    }
}