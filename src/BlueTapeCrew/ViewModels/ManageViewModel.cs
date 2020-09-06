﻿using Services.Models;
using System.Collections.Generic;

namespace BlueTapeCrew.ViewModels
{
    public class ManageViewModel
    {
        public User User { get; set; }
        public IEnumerable<Order> Orders { get; set; } 
    }
}