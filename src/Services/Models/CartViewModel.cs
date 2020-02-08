using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
    public class CartViewModel
    {
        public CartViewModel() { }
        public CartViewModel(List<CartView> items, CartTotals totals)
        {
            Items = items;
            Totals = totals;
        }

        public CartTotals Totals { get; set; } = new CartTotals();
        public List<CartView> Items { get; set; } = new List<CartView>();

        public bool IsEmpty => !Items.Any();
    }
}