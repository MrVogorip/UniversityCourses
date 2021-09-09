using System;
using System.Collections.Generic;
using TourAgency.Dal.Entities.Abstractions;

namespace TourAgency.Dal.Entities
{
    public class Tour : BaseEntity
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

        public virtual City City { get; set; }
        public virtual TypeOfHotel TypeOfHotel { get; set; }
        public virtual TypeOfTour TypeOfTour { get; set; }

        public virtual ICollection<TourCustomer> Customers { get; set; }
    }
}
