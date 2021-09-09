using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class TourViewModel : BaseEntityViewModel
    {
        [Required(ErrorMessage = "Enter a price")]
        [Range(100, 1000000, ErrorMessage = "Invalid price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Enter the max number of people")]
        [Range(1, 15, ErrorMessage = "Number of people from 1 to 15")]
        public int MaxNumberOfPeople { get; set; }
        [Required(ErrorMessage = "Is the tour hot?")]
        public bool IsHot { get; set; }

        [Required(ErrorMessage = "Enter the number of orders")]
        public int NumberOfOrders { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter the date end of tour")]
        public DateTime EndOfTour { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter the date start of tour")]
        public DateTime StartOfTour { get; set; }
        public bool IsDelete { get; set; }
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Enter a city")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Enter the type of hotel")]
        public int TypeOfHotelId { get; set; }
        [Required(ErrorMessage = "Enter the type of tour")]
        public int TypeOfTourId { get; set; }

        public CityViewModel City { get; set; }
        public TypeOfHotelViewModel TypeOfHotel { get; set; }
        public TypeOfTourViewModel TypeOfTour { get; set; }
        public ICollection<TourCustomerViewModel> Customers { get; set; }
    }
}