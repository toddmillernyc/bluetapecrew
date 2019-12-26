using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IStyleRepository
    {
        Task<List<StyleView>> GetByProductId(int id);
        Task<Style> Find(int id);
    }
}
