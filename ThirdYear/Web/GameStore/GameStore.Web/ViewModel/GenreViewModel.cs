using System.Collections.Generic;

namespace GameStore.Web.ViewModel
{
    public class GenreViewModel
    {
        public string Name { get; set; }

        public string ParentGenreId { get; set; }

        public GenreViewModel ParentGenre { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }
    }
}
