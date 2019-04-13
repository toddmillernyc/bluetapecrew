using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class MenuItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string LinkName { get; set; }
        public string ItemName { get; set; }
    }
}