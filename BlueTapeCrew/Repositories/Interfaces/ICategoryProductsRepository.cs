using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface ICategoryProductsRepository
    {
        Task<IEnumerable<Category>> Get();
    }
}
