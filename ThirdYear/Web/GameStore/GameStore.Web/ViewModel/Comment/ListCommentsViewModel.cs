using System.Collections.Generic;
using GameStore.Web.ViewModel.User;

namespace GameStore.Web.ViewModel.Comment
{
    public class ListCommentsViewModel
    {
        public string GameKey { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public UserViewModel User { get; set; }

        public CommentViewModel NewComment { get; set; }

        public bool IsAdmin { get; set; }
    }
}
