using Microsoft.AspNet.Identity.EntityFramework;
using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class Manager : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public bool IsBlock { get; set; }

    }
}
