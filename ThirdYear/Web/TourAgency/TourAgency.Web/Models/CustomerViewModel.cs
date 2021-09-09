using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class CustomerViewModel : BaseEntityViewModel
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        public string Surname { get; set; }
        public bool IsBlock { get; set; }
        public int Discount { get; set; }
        [Required(ErrorMessage = "Enter the max discount")]
        [Range(0, 75, ErrorMessage = "Discount is possible from 0 to 75")]
        public int MaxDiscount { get; set; }
        [Required(ErrorMessage = "Enter the discount step")]
        [Range(1, 10, ErrorMessage = "Discount step is possible from 1 to 10")]
        public int StepDiscount { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<TourCustomerViewModel> Tours { get; set; }
    }
}