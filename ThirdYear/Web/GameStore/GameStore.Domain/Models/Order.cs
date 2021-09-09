using System;
using System.Collections.Generic;
using GameStore.Domain.Enums;

namespace GameStore.Domain.Models
{
    public class Order : BaseEntity
    {
        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
