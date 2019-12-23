using BlueTapeCrew.Extensions;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class EmailService : IEmailService
    {
        private const string DeadLetterDir = "C:\\SMTP\\DeadLetter";

        public async Task SendEmail(SmtpRequest request)
        {
            using var client = new SmtpClient();
            var message = CreateMessage(request);
            if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Host) || request.Port == 0)
            {
                HandleDeadLetter(request);
            }
            else
            {
                await client.ConnectAsync(request.Host, request.Port).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(request.UserName, request.Password).ConfigureAwait(false);
                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        private static void HandleDeadLetter(SmtpRequest request)
        {
            var dir = Directory.CreateDirectory(DeadLetterDir);
            var fileName = "outbound-email-" + DateTime.UtcNow.ToFileTimeUtc() + ".json";
            var path = Path.Combine(dir.FullName, fileName);
            request.Password = "";
            var json = request.ToJson(true);
            File.WriteAllText(path, json);
        }

        private static MimeMessage CreateMimeMessage(string from, string to, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", from ?? ""));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            return message;
        }

        private static MimeMessage CreateMessage(SmtpRequest request)
        {
            if(request.To == null) throw new ArgumentNullException("request", "[To] property is null on the SmtpRequest Object");
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