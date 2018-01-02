using System.Linq;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Paypal;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests.Unit
{
    public class PaymentRequestTests
    {
        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsSubtotalWithToDecimalPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri,  siteSettings, Stubs.CartViewStubs.Get(), 0, "12345");
            var actual = sut.Subtotal.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsTotalWithToDecimalPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri, siteSettings, Stubs.CartViewStubs.Get(), 0, "12345");
            var actual = sut.Total.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsShippingWithToDecimalPlaces()
        {  
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri, siteSettings, CartViewStubs.Get(), 0, "12345");
            var actual = sut.Shipping.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsTaxWithToDecimalPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri, siteSettings, Stubs.CartViewStubs.Get(), 0, "12345");
            var actual = sut.Tax.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsListItemPriceToTwoPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri, siteSettings, Stubs.CartViewStubs.Get(), 0, "12345");
            var actual = sut.ItemList.items.FirstOrDefault()?.tax.Split('.')[1];

            //assert
            Assert.Equal(2, actual?.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsListItemTaxToTwoPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri, siteSettings, Stubs.CartViewStubs.Get(), 0, "12345");
            var actual = sut.ItemList.items.FirstOrDefault()?.tax.Split('.')[1];

            //assert
            Assert.Equal(2, actual?.Length);
        }

        [Fact]
        public void PaymentRequest_GivenAValidUri_SetsRedirectUrlCorrectly()
        {
            //arrange
            var siteSettings = new SiteSetting();

            //act
            var sut = new PaymentRequest(ConfigurationStubs.ProductionCheckoutUri, siteSettings, Stubs.CartViewStubs.Get(), 0, "12345");
            var actual = sut.ReturnUrl;

            //asserts
            Assert.Equal("https://bluetapecrew.com/checkoutreview", actual);
        }
    }
}
