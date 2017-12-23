using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Contracts.Repositories
{
    public interface IAccessTokenRepository
    {
        Task<AccessToken> GetFirst(TokenType tokenType);
        Task Create(AccessToken token);
        Task Delete(int id);
    }
}
