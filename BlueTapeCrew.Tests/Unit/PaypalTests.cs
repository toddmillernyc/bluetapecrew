using System;
using Xunit;

namespace BlueTapeCrew.Tests.Unit
{
    public class PaypalTests
    {
        [Fact]
        public void PaymentDetails_CreatedWithAPositiveShippingDiscount_ThrowsAnArgumentException()
        {
            //arrange
            const decimal shippingDiscount = 1m;

            //act
            try
            {

            //assert
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(ex is ArgumentOutOfRangeException);

            }
        }
    }
}
