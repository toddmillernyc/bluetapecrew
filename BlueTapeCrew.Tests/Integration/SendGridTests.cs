using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BlueTapeCrew.Tests.Integration
{
    public class SengridTests
    {
        public void Sendgrid_SendsAnEmail()
        {

        }

        public async Task<Response> SendEmail(string apiKey, string fromEmail, string fromName, string subject, string toName, string toEmail, string plainTextContent, string htmlContent)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(toName, toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
