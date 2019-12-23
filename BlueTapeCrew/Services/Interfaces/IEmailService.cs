using BlueTapeCrew.Models;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(SmtpRequest request);
    }
}