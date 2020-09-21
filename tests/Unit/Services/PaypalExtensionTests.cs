using System.Collections.Generic;
using PayPal.Api;
using Xunit;

namespace Unit.Services
{
    public class PaypalExtensionTests
    {
        [Fact]
        public void GetApprovalUrl_Returns_ApprovalUrl()
        {
            //arrange
            const string expected = "http://redirect.url.link";
            var sut = new Payment { links = new List<Links> { new Links { href = expected, rel = "approval_url" } } };

            //act
            var actual = sut.GetApprovalUrl();

            //assert
            Assert.Equal(expected, actual);
        }
    }
}
