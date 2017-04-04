using System.ComponentModel.DataAnnotations;

namespace BlueTapeCrew.ViewModels
{
    public class MenuItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string LinkName { get; set; }
        public string ItemName { get; set; }
    }
}