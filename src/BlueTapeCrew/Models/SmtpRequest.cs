using Services.Models;

namespace BlueTapeCrew.Models
{
    public class SmtpRequest
    {
        public SmtpRequest() { }

        public SmtpRequest(SiteSetting settings, string htmlBody, string textBody, string toEmail, string subject)
        {
            if (settings != null)
            {
                From = settings.SmtpUsername;
                Port = settings.SmtpPort ?? 0;
                Host = settings.SmtpHost;
                Password = settings.SmtpPassword;
                UserName = settings.SmtpUsername;
            }

            HtmlBody = htmlBody;
            TextBody = textBody;
            Subject = subject;
            To = toEmail;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        public string Subject { get; set; }
    }
}