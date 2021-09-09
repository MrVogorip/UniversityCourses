using System;

namespace GameStore.Web.ViewModel.User
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public bool IsBanned { get; set; }

        public DateTime? EndDateBanned { get; set; }
    }
}
