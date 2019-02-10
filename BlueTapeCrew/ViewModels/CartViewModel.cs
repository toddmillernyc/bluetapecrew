using BlueTapeCrew.Models.Entities;
using System.Collections.Generic;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel(List<CartView> items, CartTotals totals)
        {
            Items = items;
            Totals = totals;
        }

        public CartTotals Totals { get; set; }
        public List<CartView> Items { get; set; }
    }
}