using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class Pages
    {
        public Pages()
        {
            PageOpenGraphTags = new HashSet<PageOpenGraphTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PageOpenGraphTags> PageOpenGraphTags { get; set; }
    }
}
