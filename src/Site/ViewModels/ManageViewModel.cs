using System.Collections.Generic;
using Services.Models;

namespace Site.ViewModels
{
    public class ManageViewModel
    {
        public ManageViewModel(User user, IEnumerable<Order> orders)
        {
            User = user;
            Orders = orders;
        }

        public User User { get; set; }
        public IEnumerable<Order> Orders { get; set; } 
    }
}