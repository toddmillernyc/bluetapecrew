using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class MenuViewModel
    {
        [Key]        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public IEnumerable<MenuItemViewModel> Items { get; set; }
    }
}
