using System.ComponentModel.DataAnnotations;

namespace Site.Areas.Admin.Models
{
    public class AdminCreateUserModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}