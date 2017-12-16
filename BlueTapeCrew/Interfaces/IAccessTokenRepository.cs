using BlueTapeCrew.Models;
using System.Threading.Tasks;

namespace BlueTapeCrew.Interfaces
{
    public interface IAccessTokenRepository
    {
        Task<AccessToken> GetFirst(TokenType tokenType);
        Task Create(AccessToken token);
        Task Delete(int id);
    }
}
