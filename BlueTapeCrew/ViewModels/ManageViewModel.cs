using Entities;
using System.Collections.Generic;

namespace BlueTapeCrew.ViewModels
{
    public class ManageViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Order> Orders { get; set; } 
    }
}