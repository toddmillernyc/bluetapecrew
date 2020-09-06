using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class ReviewViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
    }
}