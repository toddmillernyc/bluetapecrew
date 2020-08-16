using System.Collections.Generic;
using Services.Models;

namespace Site.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<ProductImage> Images { get; set; } 
    }
}