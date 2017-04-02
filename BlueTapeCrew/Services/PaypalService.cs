using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Utils;

namespace BlueTapeCrew.Services
{
    public class PaypalService : IPaypalService
    {
        private const string SIGNATURE = "SIGNATURE";
        private const string PWD = "PWD";
        private const string SUBJECT = "";
        private const string HOST = "www.paypal.com";
        private const int TIMEOUT = 15000;
        private const string BN_CODE = "PP-ECWizard";

        private readonly ISiteSettingsService _siteSettingsService;

        public PaypalService(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public bool ShortcutExpressCheckout(string itemamt, string shipping, string amt, ref string token, ref string retMsg, string sessionId)
        {
            var settings = _siteSettingsService.GetSettings().Result;
            if (retMsg == null) throw new ArgumentNullException(nameof(retMsg));

            var encoder = new NvpCodec
            {
                ["METHOD"] = "SetExpressCheckout",
                ["RETURNURL"] = settings.PaypalReturnUrl,
                ["CANCELURL"] = settings.PaypalCancelUrl,
                ["BRANDNAME"] = "Blue Tape Crew",
                ["PAYMENTREQUEST_0_AMT"] = amt,
                ["PAYMENTREQUEST_0_ITEMAMT"] = itemamt,
                ["PAYMENTREQUEST_0_SHIPPINGAMT"] = shipping,
                ["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale",
                ["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD"
            };

            using (var db = new BtcEntities())
            {
                var myOrderList = db.CartViews.Where(x => x.CartId.Equals(sessionId)).ToList();

                for (var i = 0; i < myOrderList.Count(); i++)
                {
                    encoder["L_PAYMENTREQUEST_0_NAME" + i] = myOrderList[i].ProductName;
                    encoder["L_PAYMENTREQUEST_0_AMT" + i] = $"{myOrderList[i].Price:n2}";
                    encoder["L_PAYMENTREQUEST_0_QTY" + i] = myOrderList[i].Quantity.ToString();
                }
            }

            var pStrrequestforNvp = encoder.Encode();
            var pStresponsenvp = HttpCall(pStrrequestforNvp);

            var decoder = new NvpCodec();
            decoder.Decode(pStresponsenvp);

            var strAck = decoder["ACK"].ToLower();
            if (strAck == "success" || strAck == "successwithwarning")
            {
                token = decoder["TOKEN"];
                var ecurl = "https://" + HOST + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;
                retMsg = ecurl;
                return true;
            }
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                     "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                     "Desc2=" + decoder["L_LONGMESSAGE0"];
            return false;
        }

        public bool DoCheckoutPayment(string finalPaymentAmount, string token, string payerId, ref NvpCodec decoder, ref string retMsg)
        {
            if (decoder == null) throw new ArgumentNullException(nameof(decoder));
            var encoder = new NvpCodec
            {
                ["METHOD"] = "DoExpressCheckoutPayment",
                ["TOKEN"] = token,
                ["PAYERID"] = payerId,
                ["PAYMENTREQUEST_0_AMT"] = finalPaymentAmount,
                ["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD",
                ["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale"
            };

            var pStrrequestforNvp = encoder.Encode();
            var pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NvpCodec();
            decoder.Decode(pStresponsenvp);

            var strAck = decoder["ACK"].ToLower();
            if (strAck == "success" || strAck == "successwithwarning")
            {
                return true;
            }
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                     "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                     "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }

        public string HttpCall(string nvpRequest)
        {
            var settings = _siteSettingsService.GetSettings().Result;

            var strPost = nvpRequest + "&" + BuildCredentialsNvpString();
            strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BN_CODE);

            var objRequest = (HttpWebRequest)WebRequest.Create(settings.PaypalEndpointUrl);
            objRequest.Timeout = TIMEOUT;
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;

            try
            {
                using (var myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception)
            {
                // No logging for this tutorial.
            }

            //Retrieve the Response returned from the NVP API call to PayPal.
            var objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (var sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        public async Task<string> BuildCredentialsNvpString()
        {
            var settings = await _siteSettingsService.GetSettings();
            var codec = new NvpCodec
            {
                ["USER"] = settings.PaypalApiUsername,
                [PWD] = settings.PaypalApiPassword,
                [SIGNATURE] = settings.PaypalApiSignature,
                ["SUBJECT"] = SUBJECT,
                ["VERSION"] = "123.0"
            };
            return codec.Encode();
        }
    }
}