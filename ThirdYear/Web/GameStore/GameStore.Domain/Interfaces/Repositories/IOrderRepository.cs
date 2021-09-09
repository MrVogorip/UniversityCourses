using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        ICollection<Order> GetAllByCustomerId(string customerId);

        bool IsExist(string orderId);
    }
}
