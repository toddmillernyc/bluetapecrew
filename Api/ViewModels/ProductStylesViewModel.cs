using Api.Models.Entities;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class ProductStylesViewModel
    {
        public IEnumerable<StyleView> Styles { get; set; }
        public IEnumerable<Colors> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
    }
}
