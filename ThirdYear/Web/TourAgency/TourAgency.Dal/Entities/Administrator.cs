using Microsoft.AspNet.Identity.EntityFramework;
using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class Administrator : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
