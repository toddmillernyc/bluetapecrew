using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Cart")]
    public class Cart
    {
        public int Id { get; set; }

        [Column("CartId")] //todo: change in database
        public string SessionId { get; set; }
        public int StyleId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Style Style { get; set; }
    }
}
