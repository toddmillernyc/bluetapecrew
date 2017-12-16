using BlueTapeCrew.Models;
using System.Threading.Tasks;

namespace BlueTapeCrew.Interfaces
{
    public interface IPaypalApiClient
    {
        Task<AccessToken> GetAccessToken();
        void Configure(string url, string clientId, string clientSecret);
    }
}
