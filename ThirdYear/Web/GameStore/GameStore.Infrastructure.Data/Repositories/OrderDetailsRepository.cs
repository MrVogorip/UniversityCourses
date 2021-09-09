using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly GameStoreContext _gameStoreContext;

        public OrderDetailsRepository(GameStoreContext gameStoreContext)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
        }

        public ICollection<OrderDetails> GetAllByOrderId(string orderId)
        {
            ICollection<OrderDetails> orderDetailsInDb = _gameStoreContext.Set<OrderDetails>()
                        .Where(orderDetail => orderDetail.OrderId == orderId && !orderDetail.IsDeleted)
                        .ToList();

            return orderDetailsInDb;
        }
    }
}
