using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IViewModelService
    {
        Task<HomeViewModel> GetHomeViewModel();
        Task<IEnumerable<MenuViewModel>> GetSidebarViewModel();
        Task<LayoutViewModel> GetLayoutViewModel();
        Task<ProductViewModel> GetProductViewModel(string name);
    }
}