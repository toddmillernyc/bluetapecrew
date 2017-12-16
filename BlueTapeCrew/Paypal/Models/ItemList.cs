using System.Collections.Generic;

namespace BlueTapeCrew.Paypal.Models
{
    public class ItemList
    {
        public ItemList()
        {
            Items = new List<Item>();
        }

        public IList<Item> Items { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}