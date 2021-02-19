using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Logging;
using PayPal;
using PayPal.Api;
using Services.Extensions;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class PaypalService : IPaypalService
    {
        private readonly ILogger<PaypalService> _logger;

        public PaypalService(ILogger<PaypalService> logger)
        {
            _logger = logger;
        }

        private static Dictionary<string, string> GetConfig(string mode)
        {
            return new Dictionary<string, string> { { "mode", mode } };
        }

        public string GetAccessToken(string clientId, string clientSecret, string mode)
        {
            var config = GetConfig(mode);
            var credential = new OAuthTokenCredential(clientId, clientSecret, config);
            var accessToken = string.Empty;
            try
            { 
                accessToken = credential.GetAccessToken();
                if (string.IsNullOrEmpty(accessToken)) 
                    throw new PayPalException("Paypal returned empty access token");
                _logger.LogInformation("Access token retrieved from Paypal");
            }
            catch (Exception ex)
            {
                ex = ex.ToInner();
                _logger.LogError(ex, ex.Message);
            }
            return accessToken;
        }

        public APIContext GetApiContext(PaymentRequest paymentRequest)
        {
            var accessToken = paymentRequest.AccessToken ?? GetAccessToken(paymentRequest.ClientId,
                                        paymentRequest.ClientSecret, paymentRequest.Mode);
            var apiContext = new APIContext(accessToken)
            {
                Config = GetConfig(paymentRequest.Mode)
            };
            return apiContext;
        }

        public Payment GetPayment(PaymentRequest paymentRequest)
        {
            var details = new Details
            {
                tax = paymentRequest.Tax,
                shipping = paymentRequest.Shipping,
                subtotal = paymentRequest.Subtotal
            };

            var amount = new Amount
            {
                currency = paymentRequest.Currency,
                total = paymentRequest.Total.ToString(CultureInfo.InvariantCulture),
                details = details
            };

            var transaction = new Transaction
            {
                description = paymentRequest.PaymentDescription,
                invoice_number = paymentRequest.InvoiceNumber,
                amount = amount,
                item_list = paymentRequest.ItemList
            };

            var redirectUrls = new RedirectUrls
            {
                cancel_url = paymentRequest.ReturnUrl + "?cancel=true",
                return_url = paymentRequest.ReturnUrl
            };

            var payment = new Payment
            {
                intent = paymentRequest.Intent,
                payer = new Payer { payment_method = paymentRequest.PaymentMethod },
                transactions = new List<Transaction> { transaction },
                redirect_urls = redirectUrls
            };
            return payment;
        }

        public string PayWithPaypal(PaymentRequest paymentRequest)
        {
            _logger.LogInformation("Pay with PayPal");
            try
            {
                _logger.LogInformation("Retrieving PayPal ApiContext");
                var apiContext = GetApiContext(paymentRequest);

                _logger.LogInformation("Getting Paypal Payment");
                var payment = GetPayment(paymentRequest);

                _logger.LogInformation("Creating Paypal Payment");
                var createdPayment = payment.Create(apiContext);

                _logger.LogInformation("Approving Paypal; payment.");
                var redirectUrl = createdPayment.GetApprovalUrl();

                _logger.LogInformation("Returning Paypal redirect Url", redirectUrl);
                return redirectUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during PayPal checkout");
                throw;
            }
        }

        public Payment CompletePayment(CompletePaymentRequest paymentRequest)
        {
            var apiContext = new APIContext(paymentRequest.Token);
            var paymentExecution = new PaymentExecution
            {
                payer_id = paymentRequest.PayerId
            };
            var payment = new Payment
            {
                id = paymentRequest.PaymentId,
            };
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            return executedPayment;
        }
    }
}