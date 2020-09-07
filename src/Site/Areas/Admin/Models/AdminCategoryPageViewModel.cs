using System.Collections.Generic;

namespace Site.Areas.Admin.Models
{
    public class AdminCategoryPageViewModel
    {
        public AdminCategoryPageViewModel(IEnumerable<AdminCategoryViewModel> adminCategoryViewModels)
        {
            AdminCategories = adminCategoryViewModels;
        }

        public IEnumerable<AdminCategoryViewModel> AdminCategories { get; }
    }
}