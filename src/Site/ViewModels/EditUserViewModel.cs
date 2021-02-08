using System.ComponentModel.DataAnnotations;
using Site.Security.Identity;

namespace Site.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        public EditUserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            PhoneNumber = user.PhoneNumber;
            UserName = user.UserName;
            State = user.State;
            Email = user.Email;
            LockoutEnabled = user.LockoutEnabled;
            PostalCode = user.PostalCode;
            City = user.City;
            Address = user.Address;
            PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            TwoFactorEnabled = user.TwoFactorEnabled;
        }

        public ApplicationUser Map(ApplicationUser user)
        {
            user.PhoneNumber = PhoneNumber;
            user.UserName = UserName;
            user.State = State;
            user.Email = Email;
            user.LockoutEnabled = LockoutEnabled;
            user.PostalCode = PostalCode;
            user.City = City;
            user.Address = Address;
            user.PhoneNumberConfirmed = PhoneNumberConfirmed;
            user.TwoFactorEnabled = TwoFactorEnabled;
            return user;
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}
