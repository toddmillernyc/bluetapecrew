using System.Threading.Tasks;
using BlueTapeCrew.Utils;

namespace BlueTapeCrew.Paypal
{
    public interface IPaypalService
    {
        bool ShortcutExpressCheckout(string itemamt, string shipping, string amt, ref string token, ref string retMsg, string sessionId);

        bool DoCheckoutPayment(string finalPaymentAmount, string token, string payerId, ref NvpCodec decoder, ref string retMsg);

        string HttpCall(string nvpRequest);

        Task<string> BuildCredentialsNvpString();
    }
}