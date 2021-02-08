using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuCategory>> Get();
        Task<IEnumerable<MobileCategory>> GetMobileMenu();
    }
}
