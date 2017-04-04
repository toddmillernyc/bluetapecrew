namespace BlueTapeCrew.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Style
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Style()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int SizeId { get; set; }

        public int ColorId { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual Color Color { get; set; }

        public virtual Product Product { get; set; }

        public virtual Size Size { get; set; }
    }
}
