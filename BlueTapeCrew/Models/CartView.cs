namespace BlueTapeCrew.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CartView")]
    public class CartView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(68)]
        public string CartId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string LinkName { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StyleId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(25)]
        public string ColorText { get; set; }

        public string Description { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(60)]
        public string StyleText { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? SubTotal { get; set; }

        public byte[] ImageData { get; set; }
    }
}
