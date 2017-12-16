namespace BlueTapeCrew.Paypal.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToMoney(this decimal money)
        {
            return money.ToString("0.00");
        }
    }
}
