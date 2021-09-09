using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        ICollection<OrderDetails> GetAllByOrderId(string orderId);
    }
}
