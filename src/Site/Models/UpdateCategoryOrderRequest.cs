using System.Collections.Generic;

namespace Site.Models
{
    public class UpdateCategoryOrderRequest
    {
        public List<CategoryPosition> Positions { get; set; }
    }

    public class CategoryPosition
    {
        public int CategoryId { get; set; }
        public int Index { get; set; }
    }
}
