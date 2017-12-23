using System.Collections.Generic;
using System.Globalization;
using PayPal.Api;

namespace BlueTapeCrew.Paypal
{
    public class PaypalService : IPaypalService
    {
        private static Dictionary<string, string> GetConfig(string mode)
        {
            return new Dictionary<string, string> { { "mode", mode } };
        }

        public string GetAccessToken(string clientId, string clientSecret, string mode)
        {
            var accessToken = new OAuthTokenCredential(clientId, clientSecret, GetConfig(mode)).GetAccessToken();
            return accessToken;
        }

        public APIContext GetApiContext(PaymentRequest paymentRequest)
        {
            var apiContext = new APIContext(string.IsNullOrEmpty(paymentRequest.AccessToken)
                                ? GetAccessToken(paymentRequest.ClientId, paymentRequest.ClientSecret, paymentRequest.Mode)
                                : paymentRequest.AccessToken)
            { Config = GetConfig(paymentRequest.Mode) };
            return apiContext;
        }

        public Payment GetPayment(PaymentRequest paymentRequest)
        {
            return new Payment
            {
                intent = paymentRequest.Intent,
                payer = new Payer { payment_method = paymentRequest.PaymentMethod },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = paymentRequest.PaymentDescription,
                        invoice_number = paymentRequest.InvoiceNumber,
                        amount = new Amount
                        {
                            currency = paymentRequest.Currency,
                            total = paymentRequest.Total.ToString(CultureInfo.InvariantCulture),
                            details = new Details
                            {
                                tax = paymentRequest.Tax,
                                shipping = paymentRequest.Shipping,
                                subtotal = paymentRequest.Subtotal
                            }
                        },
                        item_list = paymentRequest.ItemList
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = paymentRequest.ReturnUrl + "?cancel=true",
                    return_url = paymentRequest.ReturnUrl
                }
            };
        }

        public string PaywithPaypal(PaymentRequest paymentRequest)
        {
            var apiContext = GetApiContext(paymentRequest);
            var payment = GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);
            var redirectUrl = createdPayment.GetApprovalUrl();
            return redirectUrl;
        }

        public Payment CompletePayment(CompletePaymentRequest paymentRequest)
        {
            var apiContext = new APIContext(paymentRequest.Token);
            var paymentExecution = new PaymentExecution { payer_id = paymentRequest.PayerId };
            var payment = new Payment { id = paymentRequest.PaymentId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            return executedPayment;
        }
    }
}
