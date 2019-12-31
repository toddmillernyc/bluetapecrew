using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IGuestUserService
    {
        Task<GuestUser> FindBy(string sessionId);
        Task Create(GuestUser user);
    }
}
