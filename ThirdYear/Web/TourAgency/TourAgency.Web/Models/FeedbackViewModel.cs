using System;
using TourAgency.Web.Models.Abstractions;

namespace TourAgency.Web.Models
{
    public class FeedbackViewModel : BaseEntityViewModel
    {
        public int CustomerId { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public CustomerViewModel Customer { get; set; }
    }
}