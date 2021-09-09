using System;
using System.Collections.Generic;

namespace GameStore.Web.ViewModel.Order
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public ICollection<OrderDetailsViewModel> OrderDetails { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
