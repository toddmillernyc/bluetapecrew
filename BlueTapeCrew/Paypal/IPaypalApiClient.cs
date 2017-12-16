using System.Threading.Tasks;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Paypal
{
    public interface IPaypalApiClient
    {
        Task<AccessToken> GetAccessToken();
        void Configure(string url, string clientId, string clientSecret);
    }
}
