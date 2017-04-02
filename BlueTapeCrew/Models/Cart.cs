namespace BlueTapeCrew.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cart")]
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        [StringLength(68)]
        public string CartId { get; set; }

        public int StyleId { get; set; }

        public int Count { get; set; }

        public DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        public virtual Style Style { get; set; }
    }
}
