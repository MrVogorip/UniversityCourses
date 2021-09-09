using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class ManagerViewModel : BaseEntityViewModel
    {
        [Required(ErrorMessage = "Enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter a surname")]
        public string Surname { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public bool IsBlock { get; set; }

    }
}