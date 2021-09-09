using System.Collections.Generic;

namespace GameStore.Domain.Models
{
    public class Publisher : BaseEntity
    {
        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
