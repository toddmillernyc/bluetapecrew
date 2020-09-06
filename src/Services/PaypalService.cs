﻿using System;
using System.Collections.Generic;
using System.Globalization;
using PayPal;
using PayPal.Api;
using Services.Interfaces;
using Services.Models;

namespace Services
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
            var apiContext = GetApiContext(paymentRequest);
            var payment = GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);
            var redirectUrl = createdPayment.GetApprovalUrl();
            return redirectUrl;
        }

        public Payment CompletePayment(CompletePaymentRequest paymentRequest)
        {
            try
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
            catch (PaymentsException ex)
            {
                throw new Exception(ex.Response);
            }
        }
    }
}