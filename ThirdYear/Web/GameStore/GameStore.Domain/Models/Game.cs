using System;
using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Game : BaseEntity
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public float Discount { get; set; }

        public bool IsDiscounted { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime UploadDate { get; set; }

        public int AmountViews { get; set; }

        public string PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }

        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
