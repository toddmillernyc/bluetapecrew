namespace BlueTapeCrew.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryImage
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public byte[] ImageData { get; set; }

        [StringLength(255)]
        public string MimeType { get; set; }

        public virtual Category Category { get; set; }
    }
}
