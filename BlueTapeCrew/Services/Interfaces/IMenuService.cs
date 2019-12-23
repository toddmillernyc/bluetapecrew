using BlueTapeCrew.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuCategory>> Get();
    }
}
