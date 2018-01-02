using System.Collections.Generic;
using System.Threading.Tasks;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Contracts.Repositories
{
    public interface ICategoryProductsRepository
    {
        Task<IEnumerable<Category>> Get();
    }
}
