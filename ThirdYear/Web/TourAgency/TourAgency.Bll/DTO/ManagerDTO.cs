using Microsoft.AspNet.Identity.EntityFramework;
using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    public class ManagerDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public bool IsBlock { get; set; }

    }
}
