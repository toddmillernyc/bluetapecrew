namespace Api.Models.Entities
{
    public class CategoryImages
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }

        public virtual Categories Category { get; set; }
    }
}
