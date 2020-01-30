using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IStyleRepository
    {
        Task<IEnumerable<StyleView>> GetByProductId(int id);
        Task<Style> Find(int id);
        Task Delete(int id);
        Task Create(Style style);
    }
}
