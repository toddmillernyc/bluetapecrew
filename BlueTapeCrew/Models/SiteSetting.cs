using System;

namespace BlueTapeCrew.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SiteSetting
    {
        [Required]
        [StringLength(255)]
        public string SiteTitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Keywords { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        public int Id { get; set; }

        public string AboutUs { get; set; }

        [StringLength(50)]
        public string TwitterWidgetId { get; set; }

        [StringLength(255)]
        public string GoogleClientId { get; set; }

        [StringLength(255)]
        public string GoogleClientSecret { get; set; }

        [StringLength(255)]
        public string SiteUrl { get; set; }

        [StringLength(255)]
        public string SiteLogoUrl { get; set; }

        [StringLength(255)]
        public string FacebookAppId { get; set; }

        [StringLength(255)]
        public string MailChimpApiKey { get; set; }

        [StringLength(50)]
        public string MailChimpListId { get; set; }

        public decimal FreeShippingThreshold { get; set; }
        public decimal FlatShippingRate { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmailAddress { get; set; }
        public string TwitterUrl { get; set; }
        public string FaceBookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string CopyrightText { get; set; }
        public string CopyrightUrl { get; set; }
        public string CopyrightLinktext { get; set; }
        public string MicrosoftClientId { get; set; }
        public string MicrosoftClientSecret { get; set; }
        public string FacebookClientId { get; set; }
        public string FacebookClientSecret { get; set; }
        public string TwitterClientId { get; set; }
        public string TwitterClientSecret { get; set; }
        public string InstagramClientId { get; set; }
        public string InstagramClientSecret { get; set; }

        // PayPal
        [StringLength(100)]
        public string PaypalApiUsername { get; set; }
        [StringLength(100)]
        public string PaypalClientSecret { get; set; }
        [StringLength(100)]
        public string PaypalClientId { get; set; }
        public string PaypalEndpointUrl { get; set; }
        public string PaypalReturnUrl { get; set; }
        public string PaypalCancelUrl { get; set; }

        // Paypal Sandbox
        public string PaypalSandboxAccount { get; set; }
        public string PaypalSandBoxClientId { get; set; }
        public string PaypalSandBoxSecret { get; set; }
    }
}
