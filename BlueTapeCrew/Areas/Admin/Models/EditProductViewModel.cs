using Entities;
using System.Collections.Generic;

namespace BlueTapeCrew.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<ProductImage> Images { get; set; } 
    }
}