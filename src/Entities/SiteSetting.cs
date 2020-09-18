using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("SiteSettings")]
    public class SiteSetting
    {
        // todo: remove public info from sitesetting.cs
        // begin properties copied to PublicSiteProfile.cs
        public string SiteTitle { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        public string AboutUs { get; set; }
        public string SiteUrl { get; set; }
        public string SiteLogoUrl { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmailAddress { get; set; }
        public string TwitterUrl { get; set; }
        public string FaceBookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string CopyrightText { get; set; }
        public string CopyrightUrl { get; set; }
        public string CopyrightLinktext { get; set; }
        public decimal? FreeShippingThreshold { get; set; }
        public decimal? FlatShippingRate { get; set; }
        // end properties copied to PublicSiteProfile.cs

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
