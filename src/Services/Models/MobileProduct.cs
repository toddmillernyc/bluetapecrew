namespace Services.Models
{
    public class MobileProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal LowPrice { get; set; }
        public decimal HighPrice { get; set; }
        public byte[] ImageData { get; set; }
    }
}
