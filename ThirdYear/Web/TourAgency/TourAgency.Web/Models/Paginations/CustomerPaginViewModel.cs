using System.Collections.Generic;

namespace TourAgency.Web.Models.Paginations
{
    public class CustomerPaginViewModel
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}