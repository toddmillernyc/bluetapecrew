namespace Api.Models.Entities
{
    public class AzImages
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] ImageData { get; set; }

        public virtual Product Product { get; set; }
    }
}
