using System.Threading.Tasks;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    public class PaypalTests : IntegrationTestBase
    {
        [Fact]
        public async Task GetAccessToken_GivenValidCredentials_RetrievesAccessTokenFromPaypalApi()
        {
            //arrange
            var sut = GetPaypalApiClient();

            //act
            var actual = await sut.GetAccessToken();
            
            //assert
            Assert.True(!string.IsNullOrEmpty(actual.Token));
         }

        [Fact]
        public async Task SubmitPayment_GivenAValidPaymentObjectAndToken_CreatesAPament()
        {
            //arrange
            var sut = GetPaypalApiClient();

            //act
            var accessToken = await sut.GetAccessToken();
            var request = sut.CreateOrderRequest(accessToken.Token, PaypalApi, PaypalStubs.TestPayment);
            var actual = await sut.SendOrderRequest(request);

            //assert
            Assert.Equal("created", actual.State);
        }


    }
}
