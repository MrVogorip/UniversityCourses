using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModel.Comment
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Body { get; set; }

        public bool IsQuoted { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        public string ParentCommentId { get; set; }

        public CommentViewModel ParentComment { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
