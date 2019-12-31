using System.Collections.Generic;

namespace Services.Models
{
    public class Style
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }
        public Color Color { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
    }
}
