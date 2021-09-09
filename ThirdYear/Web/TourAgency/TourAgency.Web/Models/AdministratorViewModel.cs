using Microsoft.AspNet.Identity.EntityFramework;
using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class AdministratorViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}