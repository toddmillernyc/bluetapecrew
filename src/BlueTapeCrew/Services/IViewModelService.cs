using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.ViewModels;
using Services.Models;

namespace BlueTapeCrew.Services
{
    public interface IViewModelService
    {
        Task<HomeViewModel> GetHomeViewModel();
        Task<IEnumerable<MenuViewModel>> GetSidebarViewModel();
        Task<LayoutViewModel> GetLayoutViewModel();
        Task<ProductViewModel> GetProductViewModel(string name);
    }
}