using Services.Models;

namespace Site.Extensions
{
    public static class OrderExtensions
    {
        public static void UpdateUser(this Order order, GuestUser user)
        {
            order.Email = user.Email;
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.Address = user.Address;
            order.City = user.City;
            order.State = user.State;
            order.Zip = user.PostalCode;
            order.Phone = user.PhoneNumber;
        }

        public static void UpdateUser(this Order order, User user)
        {
            order.UserName = user.Email;
            order.Email = user.Email;
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.Address = user.Address;
            order.City = user.City;
            order.State = user.State;
            order.Zip = user.PostalCode;
            order.Phone = user.PhoneNumber;
        }
    }
}