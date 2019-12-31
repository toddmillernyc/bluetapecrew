using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repositories.Entities
{
    [Table("Reviews")]
    public class Review
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ReviewText { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal Rating { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
