using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface IColorRepository
    {
        Task Create(Color color);
        Task<IEnumerable<Color>> Get();
    }
}
