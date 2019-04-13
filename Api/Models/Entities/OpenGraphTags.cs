using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class OpenGraphTags
    {
        public OpenGraphTags()
        {
            PageOpenGraphTags = new HashSet<PageOpenGraphTags>();
        }

        public int Id { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PageOpenGraphTags> PageOpenGraphTags { get; set; }
    }
}
