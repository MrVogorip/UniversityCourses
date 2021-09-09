using System.Collections.Generic;

namespace TourAgency.Web.Models.Paginations
{
    public class TourPaginViewModel
    {
        public List<TourViewModel> Tours { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}