using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public IList<string> AdditionalImages { get; set; } 
        public decimal AverageReview { get; set; }
        public IList<BestSellerViewModel> BestSellers { get; set; } 
        public string Category { get; set; }
        public string Description { get; set; }
        public List<MenuViewModel> Menu { get; set; }
        public string ImgSource { get; set; }
        public string Name { get; set; }
        public string LinkName { get; set; }
        public string Price { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
        public SelectList StyleId { get; set; }


    }
}