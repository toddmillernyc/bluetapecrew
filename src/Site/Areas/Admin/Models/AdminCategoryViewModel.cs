using System.Collections.Generic;
using Site.Areas.Admin.Models;

namespace Site.Areas.Admin.Models
{
    public class AdminCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ImageId { get; set; }
        public IEnumerable<AdminProductViewModel> Products { get; set; }
        public bool Published { get; set; }
        public int Position { get; set; }
    }
}