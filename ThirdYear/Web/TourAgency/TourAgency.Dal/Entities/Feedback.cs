using System;
using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class Feedback : BaseEntity
    {
        public int CustomerId { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
