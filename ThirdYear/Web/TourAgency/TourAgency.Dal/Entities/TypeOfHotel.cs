using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class TypeOfHotel : BaseEntity
    {
        public string Type { get; set; }
        public int NumberOfStars { get; set; }
        public bool IsDelete { get; set; }
    }
}
