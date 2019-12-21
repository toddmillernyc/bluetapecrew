using System.ComponentModel.DataAnnotations.Schema;

namespace BlueTapeCrew.Models.Entities
{
    [Table("StyleView")]
    public class StyleView
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int SizeOrder { get; set; }
        public string SizeText { get; set; }
        public int ColorId { get; set; }
        public string ColorText { get; set; }
        public decimal Price { get; set; }
        public string StyleText { get; set; }
    }
}
