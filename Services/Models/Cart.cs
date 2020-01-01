using System;

namespace Services.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int StyleId { get; set; }
        public int Count { get; set; }
        public string SessionId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public Style Style { get; set; }
    }
}
