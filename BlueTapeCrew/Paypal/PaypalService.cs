using System.Collections.Generic;
using System.Globalization;
using PayPal.Api;

namespace BlueTapeCrew.Paypal
{
    public class PaypalService : IPaypalService
    {
        // Create the configuration map that contains mode and other optional configuration details.
        public static Dictionary<string, string> GetConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        public string GetAccessToken(string clientId, string clientSecret)
        {
            var accessToken = new OAuthTokenCredential(clientId, clientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public APIContext GetApiContext(PaymentRequest paymentRequest)
        {
            var apiContext = new APIContext(string.IsNullOrEmpty(paymentRequest.AccessToken)
                                ? GetAccessToken(paymentRequest.ClientId, paymentRequest.ClientSecret)
                                : paymentRequest.AccessToken)
            { Config = GetConfig() };
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
                    cancel_url = paymentRequest.ReturnUrl + "&cancel=true",
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

        public void Run(PaymentRequest paymentRequest)
        {
            var payerId = "";
            var apiContext = GetApiContext(paymentRequest);
            var payment = GetPayment(paymentRequest);
            var createdPayment = payment.Create(apiContext);
            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            //Session.Add(guid, createdPayment.id);
            //Session.Add("flow-" + guid, this.flow);
            //else if payer id not null
            //{
            //    //var guid = request.Params["guid"];

            //    // ^ Ignore workflow code segment
            //    #region Track Workflow
            //    //this.flow = Session["flow-" + guid] as RequestFlow;
            //    //this.RegisterSampleRequestFlow();
            //    //this.flow.RecordApproval("PayPal payment approved successfully.");
            //    #endregion

            //    // Using the information from the redirect, setup the payment to execute.
            //    //var paymentId = Session[guid] as string;
            //    var paymentExecution = new PaymentExecution() { payer_id = payerId };
            //    var payment = new Payment() { /*id = paymentId */};

            //    // ^ Ignore workflow code segment
            //    #region Track Workflow
            //    //this.flow.AddNewRequest("Execute PayPal payment", payment);
            //    #endregion

            //    // Execute the payment.
            //    
            //    // ^ Ignore workflow code segment
            //    #region Track Workflow
            //    //this.flow.RecordResponse(executedPayment);
            //    #endregion

            //    // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
            //}
        }

    }
}
