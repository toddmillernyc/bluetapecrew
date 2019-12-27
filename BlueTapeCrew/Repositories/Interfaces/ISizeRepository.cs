using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> Get();
        Task Create(Size size);
    }
}
