using System.Collections.Generic;

namespace BlueTapeCrew.ViewModels
{
    public class LayoutViewModel
    {
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string AboutUs { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string SiteTitle { get; set; }
        public string TwitterHandle { get; set; }
        public string FaceBookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string CopyrightText { get; set; }
        public string CopyrightUrl { get; set; }
        public string CopyrightLinktext { get; set; }
        public IList<MenuViewModel> Menu { get; set; } 
    }
}