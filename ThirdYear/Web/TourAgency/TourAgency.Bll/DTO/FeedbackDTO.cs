using System;
using TourAgency.Bll.DTO.Abstractions;

namespace TourAgency.Bll.DTO
{
    public class FeedbackDTO : BaseEntityDTO
    {
        public int CustomerId { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
