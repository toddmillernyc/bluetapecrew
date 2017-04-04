using System.Collections.Generic;

namespace BlueTapeCrew.Areas.Admin.Models
{
    public class AdminCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ImageId { get; set; }
        public List<AdminProductViewModel> Products { get; set; }
        public bool Published { get; set; }
    }

    public class AdminProductViewModel
    {
        public int Id { get; set; }
        public int? ImageId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}