using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public string ParentGenreId { get; set; }

        public Genre ParentGenre { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}
