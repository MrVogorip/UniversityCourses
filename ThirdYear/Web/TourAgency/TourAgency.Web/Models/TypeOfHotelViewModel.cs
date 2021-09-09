using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class TypeOfHotelViewModel : BaseEntityViewModel
    {
        public string Type { get; set; }
        public int NumberOfStars { get; set; }
        public bool IsDelete { get; set; }
    }
}