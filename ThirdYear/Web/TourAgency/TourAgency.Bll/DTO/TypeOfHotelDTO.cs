using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    public class TypeOfHotelDTO : BaseEntityDTO
    {
        public string Type { get; set; }
        public int NumberOfStars { get; set; }
        public bool IsDelete { get; set; }
    }
}