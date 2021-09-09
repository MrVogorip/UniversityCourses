using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    public class CustomerDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsBlock { get; set; }
        public int Discount { get; set; }
        public int MaxDiscount { get; set; }
        public int StepDiscount { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<TourCustomerDTO> Tours { get; set; }
    }
}
