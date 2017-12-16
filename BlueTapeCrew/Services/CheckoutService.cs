using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
{
    public class CheckoutService : ICheckoutService
    {
        private IPaypalApiClient _paypalClient;
        private IAccessTokenRepository _tokenRepository;

        private AccessToken _paypalBearerToken;

        public CheckoutService(IPaypalApiClient paypalClient, IAccessTokenRepository tokenRepository)
        {
            _paypalClient = paypalClient;
            _tokenRepository = tokenRepository;
        }

    }
}
