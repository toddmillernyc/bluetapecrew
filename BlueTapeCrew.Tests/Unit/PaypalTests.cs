using System;
using BlueTapeCrew.Paypal.Models;
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
                var sut = new Details(0m, 0m, 0m, 0m, shippingDiscount);

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
