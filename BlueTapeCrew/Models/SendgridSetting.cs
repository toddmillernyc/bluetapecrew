namespace BlueTapeCrew.Models
{
    public class SendgridSetting
    {
        public int Id { get; set; }
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}