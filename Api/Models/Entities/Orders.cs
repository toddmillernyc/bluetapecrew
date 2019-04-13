using System;
using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
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
        public int? InvoiceId { get; set; }

        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
