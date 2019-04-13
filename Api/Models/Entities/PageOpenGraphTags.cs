namespace Api.Models.Entities
{
    public class PageOpenGraphTags
    {
        public int PageId { get; set; }
        public int OpenGraphTagId { get; set; }
        public string Content { get; set; }

        public virtual OpenGraphTags OpenGraphTag { get; set; }
        public virtual Pages Page { get; set; }
    }
}
