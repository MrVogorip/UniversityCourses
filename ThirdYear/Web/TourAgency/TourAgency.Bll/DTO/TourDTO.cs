using System;
using System.Collections.Generic;
using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    public class TourDTO : BaseEntityDTO
    {
        public int Price { get; set; }
        public int MaxNumberOfPeople { get; set; }
        public bool IsHot { get; set; }
        public int NumberOfOrders { get; set; }
        public DateTime EndOfTour { get; set; }
        public DateTime StartOfTour { get; set; }
        public bool IsDelete { get; set; }
        public string ImagePath { get; set; }

        public int CityId { get; set; }
        public int TypeOfHotelId { get; set; }
        public int TypeOfTourId { get; set; }

        public CityDTO City { get; set; }
        public TypeOfHotelDTO TypeOfHotel { get; set; }
        public TypeOfTourDTO TypeOfTour { get; set; }
        public ICollection<TourCustomerDTO> Customers { get; set; }

    }
}
