using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class MenuItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Slug { get; set; }
        public string ItemName { get; set; }
    }
}