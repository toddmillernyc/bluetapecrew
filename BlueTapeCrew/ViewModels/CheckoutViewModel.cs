using BlueTapeCrew.Models;
using System.ComponentModel.DataAnnotations;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.ViewModels
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel() { }

        public CheckoutViewModel(AspNetUser user, CartViewModel cart)
        {
            Cart = cart;
            if(user != null)
            {
                Address = user.Address;
                City = user.City;
                Email = user.Email;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Phone = user.PhoneNumber;
                State = user.State;
                Zip = user.PostalCode;
            }
        }

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