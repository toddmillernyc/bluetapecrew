using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;
using Site.ViewModels;

namespace Site.Services.Interfaces
{
    public interface IViewModelService
    {
        Task<HomeViewModel> GetHomeViewModel();
        Task<IEnumerable<MenuViewModel>> GetSidebarViewModel();
        Task<LayoutViewModel> GetLayoutViewModel();
        Task<ProductViewModel> GetProductViewModel(string name);
    }
}