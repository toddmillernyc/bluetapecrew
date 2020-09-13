using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> Get();
        Task Create(Size size);
    }
}
