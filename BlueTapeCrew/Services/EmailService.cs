using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(SmtpRequest request)
        {
            using (var client = new SmtpClient())
            {
                var message = CreateMessage(request);
                await client.ConnectAsync(request.Host, request.Port).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(request.UserName, request.Password).ConfigureAwait(false);
                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        private static MimeMessage CreateMimeMessage(string from, string to, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            return message;
        }

        private static MimeMessage CreateMessage(SmtpRequest request)
        {
            var message = CreateMimeMessage(request.From, request.To, request.Subject);
            message.Body = CreateMessageBody(request.TextBody, request.HtmlBody);
            return message;
        }

        private static MimeEntity CreateMessageBody(string textBody, string htmlBody)
        {
            var bodyBuilder = new BodyBuilder { TextBody = textBody, HtmlBody = htmlBody };
            return bodyBuilder.ToMessageBody();
        }
    }

}