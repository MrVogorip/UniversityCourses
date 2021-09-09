using Microsoft.AspNet.Identity.EntityFramework;
using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    class AdministratorDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
