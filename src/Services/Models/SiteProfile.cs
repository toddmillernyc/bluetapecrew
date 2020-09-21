namespace Services.Models
{
    public class SiteProfile
    {
        public int Id { get; set; }
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
    }
}
