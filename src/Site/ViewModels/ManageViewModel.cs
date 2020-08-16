using System.Collections.Generic;
using Services.Models;

namespace Site.ViewModels
{
    public class ManageViewModel
    {
        public User User { get; set; }
        public IEnumerable<Order> Orders { get; set; } 
    }
}