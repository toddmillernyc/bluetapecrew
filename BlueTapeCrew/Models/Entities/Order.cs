using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    [Table("Orders")]
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public decimal? Shipping { get; set; }
        public decimal? Total { get; set; }
        public DateTime? DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public decimal? SubTotal { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
