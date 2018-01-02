using System.Collections.Generic;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.ViewModels
{
    public class ManageViewModel
    {
        public AspNetUser User { get; set; }
        public IEnumerable<Order> Orders { get; set; } 
    }
}