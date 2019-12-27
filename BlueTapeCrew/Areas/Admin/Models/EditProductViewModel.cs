using Entities;
using System.Collections.Generic;

namespace BlueTapeCrew.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<ProductImage> Images { get; set; } 
    }
}