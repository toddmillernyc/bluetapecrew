namespace Api.Models.Entities
{
    public class MailSettings
    {
        public int Id { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
