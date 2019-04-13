namespace Api.Models.Entities
{
    public class ProductCategories
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
