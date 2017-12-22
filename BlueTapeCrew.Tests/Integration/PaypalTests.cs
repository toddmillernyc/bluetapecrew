using BlueTapeCrew.Paypal;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    public class PaypalTests : IntegrationTestBase
    {
        [Fact]
        public void GetApiContext_GivenAPaymentRequestWithValidApiCredentials_ReturnsAnApiContextWithAValidRequestId()
        {
            //arrange
            var sut = GetPaypalService();
            var paymentRequest = new PaymentRequest(Settings, CartViewStubs.Get(), 123, "");

            //act
            var apiContext = sut.GetApiContext(paymentRequest);

            //assert
            Assert.Equal(36, apiContext.RequestId.Length);
        }

        [Fact]
        public void PaymentCreate_GivenAValidPaymentAndApiContext_ReturnsAValidCreatedPayment()
        {
            //arrange
            var sut = GetPaypalService();
            var paymentRequest = new PaymentRequest(Settings, CartViewStubs.Get(), 123, "");

            //act
            var apiContext = sut.GetApiContext(paymentRequest);
            var payment = sut.GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);

            //assert
            Assert.Equal("created", createdPayment.state);
        }


        //[Fact]
        //public async Task SubmitPayment_GivenAValidPaymentObjectAndToken_CreatesAPament()
        //{
        //    //arrange
        //    var sut = GetPaypalApiClient();
        //    var paymentStub = PaypalStubs.TestPayment;

        //    //act
        //    var accessToken = await sut.GetAccessToken();
        //    var request = sut.CreateOrderRequest(accessToken.Token, PaypalApi, paymentStub);
        //    var actual = await sut.SendOrderRequest(request);

        //    //assert
        //    Assert.Equal("created", actual.State);
        //}


    }
}
