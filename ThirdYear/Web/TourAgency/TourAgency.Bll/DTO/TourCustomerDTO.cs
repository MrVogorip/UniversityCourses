using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    public class TourCustomerDTO : BaseEntityDTO
    {
        public int TourId { get; set; }
        public int CustomerId { get; set; }
        public int TypeOfStatusId { get; set; }
        public int RealNumberOfPeople { get; set; }
        public int RealPrice { get; set; }
        public TourDTO Tour { get; set; }
        public CustomerDTO Customer { get; set; }
        public TypeOfStatusDTO TypeOfStatus { get; set; }
    }
}
