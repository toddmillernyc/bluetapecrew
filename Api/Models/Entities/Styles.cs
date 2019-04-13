using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class Style
    {
        public Style()
        {
            Cart = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public decimal Price { get; set; }

        public virtual Colors Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
    }
}
