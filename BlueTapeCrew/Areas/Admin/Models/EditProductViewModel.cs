using System.Collections.Generic;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Image> Images { get; set; } 
    }
}