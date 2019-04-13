using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class BannerImages
    {
        public BannerImages()
        {
            LayerSliders = new HashSet<LayerSliders>();
        }

        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public string MimeType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LayerSliders> LayerSliders { get; set; }
    }
}
