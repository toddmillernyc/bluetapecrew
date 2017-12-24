using System.Threading.Tasks;

namespace BlueTapeCrew.Email
{
    public interface IEmailMessageService
    {
        Task SendEmail(string fromName, string fromEmail, string toName, string toEmail, string subject, string textBody, string htmlBody);
    }
}
