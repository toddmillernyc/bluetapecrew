using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IGuestUserRepository
    {
        Task Create(GuestUser user);
        Task<GuestUser> FindBy(string sessionId);
    }
}
