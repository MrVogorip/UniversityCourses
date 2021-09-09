using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class TourCustomer : BaseEntity
    {
        public int TourId { get; set; }
        public int CustomerId { get; set; }
        public int TypeOfStatusId { get; set; }
        public int RealNumberOfPeople { get; set; }
        public int RealPrice { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual TypeOfStatus TypeOfStatus { get; set; }
    }
}
