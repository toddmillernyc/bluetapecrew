namespace BlueTapeCrew.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string ReviewText { get; set; }

        public DateTime? DateCreated { get; set; }

        public decimal Rating { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
