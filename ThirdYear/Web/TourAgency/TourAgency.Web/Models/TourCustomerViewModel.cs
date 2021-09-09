using System.ComponentModel.DataAnnotations;
using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class TourCustomerViewModel : BaseEntityViewModel
    {
        public int TourId { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Enter the type of status")]
        public int TypeOfStatusId { get; set; }
        public int RealNumberOfPeople { get; set; }
        public int RealPrice { get; set; }
        public TourViewModel Tour { get; set; }
        public CustomerViewModel Customer { get; set; }
        public TypeOfStatusViewModel TypeOfStatus { get; set; }
    }
}