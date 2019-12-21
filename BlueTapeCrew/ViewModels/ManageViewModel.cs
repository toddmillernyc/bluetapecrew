using System.Collections.Generic;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.ViewModels
{
    public class ManageViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Order> Orders { get; set; } 
    }
}