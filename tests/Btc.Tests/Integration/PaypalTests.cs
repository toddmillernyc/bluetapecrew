using Btc.Tests.Stubs;
using Services.Interfaces;
using Services.Models;
using System.Threading.Tasks;
using Xunit;

namespace Btc.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class PaypalTests
    {
        private readonly IntegrationTextFixture _fixture;

        private readonly ISiteSettingsService _settings;

        public PaypalTests(IntegrationTextFixture fixture)
        {
            _fixture = fixture;
            _settings = _fixture.Resolve<ISiteSettingsService>();
        }

        [Fact]
        public async Task GetApiContext_GivenAPaymentRequestWithValidApiCredentials_ReturnsAnApiContextWithAValidRequestId()
        {
            //arrange
            var settings = await _settings.Get();
            var profile = await _settings.GetSiteProfile();
            var sut = _fixture.Resolve<IPaypalService>();
            var options = new PaymentRequestOptions(profile, settings, _fixture.ProductionCheckoutUri);

            var paymentRequest = new PaymentRequest(CartViewStubs.Get(), options);
            //act
            var apiContext = sut.GetApiContext(paymentRequest);

            //assert
            Assert.Equal(36, apiContext.RequestId.Length);
        }

        [Fact]
        public async Task PaymentCreate_GivenAValidPaymentAndApiContext_ReturnsAValidCreatedPayment()
        {
            //arrange
            var settings = await _settings.Get();
            var profile = await _settings.GetSiteProfile();
            var sut = _fixture.Resolve<IPaypalService>();
            var options = new PaymentRequestOptions(profile, settings, _fixture.ProductionCheckoutUri);
            var paymentRequest = new PaymentRequest(CartViewStubs.Get(), options);

            //act
            var apiContext = sut.GetApiContext(paymentRequest);
            var payment = sut.GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);

            //assert
            Assert.Equal("created", createdPayment.state);
        }

        [Fact]
        public async Task GetAccessToken_GivenSandBoxCredentials_ReturnsAccessToken()
        {
            //arrange
            var settings = await _settings.Get();
            var sut = _fixture.Resolve<IPaypalService>();

            //act
            var accessToken = sut.GetAccessToken(settings.PaypalSandBoxClientId, settings.PaypalSandBoxSecret, "sandbox");

            //assert
            Assert.True(!string.IsNullOrEmpty(accessToken));
        }

        [Fact]
        public async Task GetAccessToken_GivenLiveCredentials_ReturnsAccessToken()
        {
            //arrange
            var settings = await _settings.Get();
            var sut = _fixture.Resolve<IPaypalService>();

            //act
            var accessToken = sut.GetAccessToken(settings.PaypalClientId, settings.PaypalClientSecret, "live");

            //assert
            Assert.True(!string.IsNullOrEmpty(accessToken));
        }
    }
}
