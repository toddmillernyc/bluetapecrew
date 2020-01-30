using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(SmtpRequest request);
    }
}