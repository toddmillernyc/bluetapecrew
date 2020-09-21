using System;
using System.Collections.Generic;

namespace Services.Models
{
    public class PaymentRequestOptions
    {
        public PaymentRequestOptions(SiteProfile profile, SiteSetting settings, Uri requestUri, bool isSandbox = true)
        {
            FlatShippingRate = profile.FlatShippingRate;
            FreeShippingThreshold = profile.FreeShippingThreshold;
            InvoiceNumber = DateTime.Now.Ticks;
            IsSandbox = isSandbox;
            PaypalClientId = settings.PaypalClientId;
            PaypalClientSecret = settings.PaypalClientSecret;
            PaypalSandBoxClientId = settings.PaypalClientId;
            PaypalSandBoxSecret = settings.PaypalSandBoxSecret;
            RequestUri = requestUri;
        }

        public long InvoiceNumber { get; set; }
        public Uri RequestUri { get; set; }
        public bool IsSandbox { get; set; }
        public decimal? FreeShippingThreshold { get; set; }
        public decimal? FlatShippingRate { get; set; }
        public string PaypalSandBoxClientId { get; set; }
        public string PaypalSandBoxSecret { get; set; }
        public string PaypalClientId { get; set; }
        public string PaypalClientSecret { get; set; }
    }
}