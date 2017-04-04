namespace BlueTapeCrew.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CartImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        public virtual Product Product { get; set; }
    }
}
