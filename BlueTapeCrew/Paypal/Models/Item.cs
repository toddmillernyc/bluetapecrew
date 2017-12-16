namespace BlueTapeCrew.Paypal.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Tax { get; set; }
        public string Sku { get; set; }
        public string Currency { get; set; }
    }
}