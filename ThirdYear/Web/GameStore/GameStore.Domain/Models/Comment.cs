using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public bool IsQuoted { get; set; }

        public string UserId { get; set; }

        public string GameId { get; set; }

        public string ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
