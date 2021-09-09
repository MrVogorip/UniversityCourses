using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsBlock { get; set; }
        public int Discount { get; set; }
        public int MaxDiscount { get; set; }
        public int StepDiscount { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual ICollection<TourCustomer> Tours { get; set; }
    }
}
