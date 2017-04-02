namespace BlueTapeCrew.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        [StringLength(24)]
        public string SessionId { get; set; }

        [StringLength(15)]
        public string IpAddress { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Shipping { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? Total { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [StringLength(255)]
        public string Zip { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? SubTotal { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
