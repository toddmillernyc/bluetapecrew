using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.Models.Entities
{
    public class AzImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        public virtual Product Product { get; set; }
    }
}
