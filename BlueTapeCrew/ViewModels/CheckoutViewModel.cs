using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "eMail is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [StringLength(20, ErrorMessage = "Phone cannot be longer than 40 characters.")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }
        public CartViewModel Cart { get; set; }
    }
}