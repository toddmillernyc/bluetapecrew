using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuCategory>> Get();
    }
}
