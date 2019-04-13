using System.Collections.Generic;

namespace Api.ViewModels
{
    public class AdminCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ImageId { get; set; }
        public List<AdminProductViewModel> Products { get; set; }
        public bool Published { get; set; }
    }
}
