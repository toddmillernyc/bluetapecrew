using BlueTapeCrew.Models;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class PaypalTests
    {
        private readonly IntegrationTextFixture _fixture;

        public PaypalTests(IntegrationTextFixture fixture) { _fixture = fixture; }

        [Fact]
        public void GetApiContext_GivenAPaymentRequestWithValidApiCredentials_ReturnsAnApiContextWithAValidRequestId()
        {
            //arrange
            var paymentRequest = new PaymentRequest(_fixture.ProductionCheckoutUri, _fixture.SiteSettings, CartViewStubs.Get(), 123, "");

            //act
            var apiContext = _fixture.PaypalService.GetApiContext(paymentRequest);

            //assert
            Assert.Equal(36, apiContext.RequestId.Length);
        }

        [Fact]
        public void PaymentCreate_GivenAValidPaymentAndApiContext_ReturnsAValidCreatedPayment()
        {
            //arrange
            var paymentRequest = new PaymentRequest(_fixture.ProductionCheckoutUri, _fixture.SiteSettings, CartViewStubs.Get(), 123, "");

            //act
            var apiContext = _fixture.PaypalService.GetApiContext(paymentRequest);
            var payment = _fixture.PaypalService.GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);

            //assert
            Assert.Equal("created", createdPayment.state);
        }

        [Fact]
        public void GetAccessToken_GivenSandBoxCredentials_ReturnsAccessToken()
        {
            //arrange
            var settings = _fixture.SiteSettings;

            //act
            var accessToken = _fixture.PaypalService.GetAccessToken(settings.PaypalSandBoxClientId, settings.PaypalSandBoxSecret, "sandbox");

            //assert
            Assert.True(!string.IsNullOrEmpty(accessToken));
        }

        [Fact]
        public void GetAccessToken_GivenLiveCredentials_ReturnsAccessToken()
        {
            //arrange
            var settings = _fixture.SiteSettings;

            //act
            var accessToken = _fixture.PaypalService.GetAccessToken(settings.PaypalClientId, settings.PaypalClientSecret, "live");

            //assert
            Assert.True(!string.IsNullOrEmpty(accessToken));
        }
    }
}
