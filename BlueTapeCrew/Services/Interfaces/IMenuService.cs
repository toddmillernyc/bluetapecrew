using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuCategory>> Get();
    }
}
