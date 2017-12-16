using System.Threading.Tasks;
using BlueTapeCrew.Services;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    public class PaypalTests : IntegrationTestBase
    {
        [Fact]
        public async Task GetAccessToken_GivenValidCredentials_RetrievesAccessTokenFromPaypalApi()
        {
            //arrange
            var sut = new PaypalApiClient(WebService, AccessTokenRepository);
            sut.Configure(PaypalSandboxUrl, Settings.PaypalSandBoxClientId, Settings.PaypalSandBoxSecret);

            //act
            var actual = await sut.GetAccessToken();
            
            //assert
            Assert.True(!string.IsNullOrEmpty(actual.Token));
         }
    }
}
