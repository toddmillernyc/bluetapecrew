using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.Models.Entities
{
    public class MailSetting
    {
        public int Id { get; set; }

        public int Port { get; set; }

        [Required]
        [StringLength(255)]
        public string SmtpServer { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
