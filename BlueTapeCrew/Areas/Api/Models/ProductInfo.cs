namespace BlueTapeCrew.Areas.Api.Models
{
    public class ProductInfo
    {
        public ProductInfo(BlueTapeCrew.Models.Entities.Product model)
        {
            Name = model.ProductName;
            Slug = model.LinkName;
            Description = model.Description;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}