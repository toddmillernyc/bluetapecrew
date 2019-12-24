using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IGuestUserRepository
    {
        Task Create(GuestUser user);
        Task<GuestUser> FindBy(string sessionId);
    }
}
