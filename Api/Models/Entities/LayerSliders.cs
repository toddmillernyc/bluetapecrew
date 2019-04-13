namespace Api.Models.Entities
{
    public class LayerSliders
    {
        public int Id { get; set; }
        public string TitleContent { get; set; }
        public string MiniTextContent { get; set; }
        public int BannerImageId { get; set; }
        public string LinkUrl { get; set; }

        public virtual BannerImages BannerImage { get; set; }
    }
}
