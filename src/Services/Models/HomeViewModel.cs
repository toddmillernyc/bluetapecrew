using System.Collections.Generic;

namespace Services.Models
{
    public class HomeViewModel
    {
        public string SiteTitle { get; set; }
        public string Description { get; set; }
        public IEnumerable<CatalogModel> Catalog { get; set; }
    }
}