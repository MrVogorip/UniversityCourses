using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Platform : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}
