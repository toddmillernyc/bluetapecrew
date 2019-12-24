using Entities;
using System.ComponentModel.DataAnnotations;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.ViewModels
{
    public class CheckoutRequest
    {
        public string ReturnUrl { get; }

        public CheckoutRequest() { }

        public CheckoutRequest(User user, CartViewModel cart, string returnUrl)
        {
            ReturnUrl = returnUrl;
            Cart = cart;
            if (user == null) return;

            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.PhoneNumber;
            Address = user.Address;
            City = user.City;
            State = user.State;
            PostalCode = user.PostalCode;
        }

        public CheckoutRequest(GuestUser user, CartViewModel cart)
        {
            Cart = cart;
            if (user == null) return;

            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.PhoneNumber;
            Address = user.Address;
            City = user.City;
            State = user.State;
            PostalCode = user.PostalCode;
        }

        public string UserName { get; set; }

        public string SessionId { get; set; }

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
        public string PostalCode { get; set; }

        [StringLength(20, ErrorMessage = "Phone cannot be longer than 40 characters.")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }

        public CartViewModel Cart { get; set; }
    }
}