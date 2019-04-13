using System;

namespace Api.Models.Entities
{
    public class TodoItems
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Complete { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
