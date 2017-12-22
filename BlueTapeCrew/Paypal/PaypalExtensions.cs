using System.Linq;
using PayPal.Api;

namespace BlueTapeCrew.Paypal
{
    public static class PaypalExtensions
    {
        private const string ApprovalUrlKey = "approval_url";

        public static string GetApprovalUrl(this Payment payment)
        {
            return payment
                        .links
                        .FirstOrDefault(x => x.rel == ApprovalUrlKey)
                        ?.href;
        }
    }
}
