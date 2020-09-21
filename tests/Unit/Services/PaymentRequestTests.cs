using System.Linq;
using Services.Models;
using Stubs;
using Xunit;

namespace Unit.Services
{
    public class PaymentRequestTests
    {
        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsSubtotalWithToDecimalPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.Subtotal.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsTotalWithToDecimalPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.Total.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsShippingWithToDecimalPlaces()
        {  
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.Shipping.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsTaxWithToDecimalPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.Tax.Split('.')[1];

            //assert
            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsListItemPriceToTwoPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.ItemList.items.FirstOrDefault()?.tax.Split('.')[1];

            //assert
            Assert.Equal(2, actual?.Length);
        }

        [Fact]
        public void PaymentRequest_GivenValidConstructorArguments_FormatsListItemTaxToTwoPlaces()
        {
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.ItemList.items.FirstOrDefault()?.tax.Split('.')[1];

            //assert
            Assert.Equal(2, actual?.Length);
        }

        [Fact]
        public void PaymentRequest_GivenAValidUri_SetsRedirectUrlCorrectly()
        {
            //arrange
            var siteSettings = new SiteSetting();
            var siteProfile = new SiteProfile();

            //act
            var options = new PaymentRequestOptions(siteProfile, siteSettings, ConfigurationStubs.ProductionCheckoutUri);
            var sut = new PaymentRequest(CartViewStubs.Get(), options);
            var actual = sut.ReturnUrl;

            //asserts
            Assert.Equal("https://bluetapecrew.com/checkoutreview", actual);
        }
    }
}
