using System;

namespace GameStore.Domain.Models
{
    public class User : BaseEntity
    {
        public bool IsBanned { get; set; }

        public DateTime? EndDateBanned { get; set; }
    }
}
