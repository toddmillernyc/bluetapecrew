using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Paypal.Models;
using BlueTapeCrew.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;

namespace BlueTapeCrew.Paypal
{
    public class PaypalApiClient : IPaypalApiClient
    {
        private const string AuthorizationKey = "Authorization";
        private const string AuthorizationValue = "Basic";
        private const string GrantTypeKey = "grant_type";
        private const string GrantTypeValue = "client_credentials";

        private string _url;
        private string _clientId;
        private string _clientSecret;
        private string _accessToken;
        private string _credentials;

        private HttpClient _client;
        private IWebService _webService;
        private IAccessTokenRepository _tokenRepository;
        private AccessToken _paypalBearerToken;
        private JsonSerializerSettings _jsonSerializerSettings;

        public PaypalApiClient(IWebService webService, IAccessTokenRepository tokenRepository)
        {
            _webService = webService;
            _tokenRepository = tokenRepository;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new SnakeCaseContractResolver()
            };
        }

        public void Configure(string url, string clientId, string clientSecret)
        {
            _url = url;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _credentials = _webService.FormatAuthorizationCredentials(clientId, clientSecret);

            _client = new HttpClient();
        }

        public void SetAccessToken(string token)
        {
            _accessToken = token;
        }

        public async Task<AccessToken> GetAccessToken()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = CreateOauthTokenRequest();
            var response = await _client.SendAsync(request);
            var accessToken = await ParseAccessTokenFrom(response);
            return accessToken;
        }

        public async Task<AccessToken> ParseAccessTokenFrom(HttpResponseMessage response)
        {
            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
            var tokenString = payload.Value<string>("access_token");
            var expiresIn = payload.Value<long>("expires_in");
            var accessToken = new AccessToken(tokenString, expiresIn - 600); //subtract ten minutes for safety
            return accessToken;
        }

        public HttpRequestMessage CreateOauthTokenRequest()
        {
            var requestContent = new FormUrlEncodedContent(new Dictionary<string, string> { { GrantTypeKey, GrantTypeValue } });
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_url}oauth2/token") { Content = requestContent };
            request.Headers.Add(AuthorizationKey, $"{AuthorizationValue} {_credentials}");
            return request;
        }

        public HttpRequestMessage CreateOrderRequest(string accessToken, string apiBase, Payment payment)
        {
            var requestBody = JsonConvert.SerializeObject(payment, _jsonSerializerSettings);
            var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, $"{apiBase}payments/payment") { Content = requestContent };
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            return request;
        }

        public async Task<PaymentResponse> SendOrderRequest(HttpRequestMessage request)
        {
            var response = await _client.SendAsync(request);
            return !response.IsSuccessStatusCode 
                    ? null 
                    : JsonConvert.DeserializeObject<PaymentResponse>(await response.Content.ReadAsStringAsync());
        }

        private async Task RefreshPaypalAccessToken()
        {
            var storedToken = await _tokenRepository.GetFirst(TokenType.Paypal);
            if (storedToken == null) await CreateNewAccessToken();
            else if (storedToken.IsExpired())
            {
                await _tokenRepository.Delete(storedToken.Id);
                await CreateNewAccessToken();
            }
        }

        private async Task CreateNewAccessToken()
        {
            var token = await GetAccessToken();
            await _tokenRepository.Create(token);
            _paypalBearerToken = token;
        }
    }
}
