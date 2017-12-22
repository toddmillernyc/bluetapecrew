using System;
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
            var paymentRequest = new PaymentRequest(ProductionCheckoutUri, Settings, CartViewStubs.Get(), 123, "");

            //act
            var apiContext = PaypalService.GetApiContext(paymentRequest);

            //assert
            Assert.Equal(36, apiContext.RequestId.Length);
        }

        [Fact]
        public void PaymentCreate_GivenAValidPaymentAndApiContext_ReturnsAValidCreatedPayment()
        {
            //arrange
            var sut = PaypalService;
            var paymentRequest = new PaymentRequest(ProductionCheckoutUri, Settings, CartViewStubs.Get(), 123, "");

            //act
            var apiContext = sut.GetApiContext(paymentRequest);
            var payment = sut.GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);

            //assert
            Assert.Equal("created", createdPayment.state);
        }
    }
}
