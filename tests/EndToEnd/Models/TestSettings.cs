namespace EndToEnd.Models
{
    public class TestSettings
    {
        public string BaseUrl { get; set; }
        public string ConnectionString { get; set; }
        public string DeadLetterPath { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public int ImplicitWait { get; set; }
        public string PaypalSettingsPath { get; set; }
    }
}