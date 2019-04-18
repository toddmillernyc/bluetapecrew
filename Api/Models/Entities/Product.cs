using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Models.Entities
{
    public class Product
    {
        public Product()
        {
            CartImages = new HashSet<CartImages>();
            ProductCategories = new HashSet<ProductCategories>();
            ProductImages = new HashSet<ProductImage>();
            Reviews = new HashSet<Review>();
            Styles = new HashSet<Style>();
        }

        public int Id { get; set; }
        public int? ImageId { get; set; }

        [JsonProperty("name")]
        public string ProductName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("slug")]
        public string LinkName { get; set; }

        public virtual Image Image { get; set; }
        public virtual AzImages AzImages { get; set; }
        public virtual ICollection<CartImages> CartImages { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Style> Styles { get; set; }
    }
}
