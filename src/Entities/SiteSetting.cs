using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("SiteSettings")]
    public class SiteSetting
    {
        public int Id { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string FacebookAppId { get; set; }
        public string MailChimpApiKey { get; set; }
        public string MailChimpListId { get; set; }
        public string PaypalApiUsername { get; set; }
        public string MicrosoftClientId { get; set; }
        public string MicrosoftClientSecret { get; set; }
        public string FacebookClientId { get; set; }
        public string FacebookClientSecret { get; set; }
        public string TwitterClientId { get; set; }
        public string TwitterClientSecret { get; set; }
        public string InstagramClientId { get; set; }
        public string InstagramClientSecret { get; set; }
        public string PaypalEndpointUrl { get; set; }
        public string PaypalSandboxAccount { get; set; }
        public string PaypalSandBoxClientId { get; set; }
        public string PaypalSandBoxSecret { get; set; }
        public string PaypalClientSecret { get; set; }
        public string PaypalClientId { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int? SmtpPort { get; set; }
    }
}
