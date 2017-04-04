using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.ViewModels;

namespace BlueTapeCrew.Interfaces
{
    public interface IViewModelService
    {
        Task<HomeViewModel> GetHomeViewModel();
        Task<IEnumerable<MenuViewModel>> GetSidebarViewModel();
        Task<LayoutViewModel> GetLayoutViewModel();
    }
}