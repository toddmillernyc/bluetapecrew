using Api.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Repositories.Interfaces
{
    public interface IStyleViewRepository
    {
        Task<IEnumerable<StyleView>> GetBy(int productId);
    }
}
