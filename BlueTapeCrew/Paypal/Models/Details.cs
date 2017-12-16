using System;
using BlueTapeCrew.Paypal.Extensions;

namespace BlueTapeCrew.Paypal.Models
{
    public class Details
    {
        private const string ShippingDiscountError = "Shipping discout must be 0 or a negative number";

        public Details(decimal subTotal,
                       decimal tax,
                       decimal shipping,
                       decimal handling = 0,
                       decimal shippingDiscount = 0,
                       decimal insurance = 0)
        {
            if(shippingDiscount > 0)
                throw new ArgumentOutOfRangeException(ShippingDiscountError);

            Subtotal = subTotal.ToMoney();
            Tax = tax.ToMoney();
            Shipping = shipping.ToMoney();
            HandlingFee = handling.ToMoney();
            ShippingDiscount = shippingDiscount.ToMoney();
            Insurance = insurance.ToMoney();
        }

        public string Subtotal { get; set; }
        public string Tax { get; set; }
        public string Shipping { get; set; }
        public string HandlingFee { get; set; }
        public string ShippingDiscount { get; set; }
        public string Insurance { get; set; }

    }
}