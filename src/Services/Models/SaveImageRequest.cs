namespace Services.Models
{
    public class SaveImageRequest
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] ImageData { get; set; }
    }
}
