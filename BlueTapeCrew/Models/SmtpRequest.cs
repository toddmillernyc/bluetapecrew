namespace BlueTapeCrew.Models
{
    public class SmtpRequest
    {
        public SmtpRequest() { }

        public SmtpRequest(SiteSetting settings, string htmlBody, string textBody, string toEmail)
        {
            From = settings.SmtpUsername;
            HtmlBody = htmlBody;
            TextBody = textBody;
            Subject = "Your BlueTapeCrew.com order";
            To = toEmail;
            Port = settings.SmtpPort;
            Host = settings.SmtpHost;
            Password = settings.SmtpPassword;
            UserName = settings.SmtpUsername;
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