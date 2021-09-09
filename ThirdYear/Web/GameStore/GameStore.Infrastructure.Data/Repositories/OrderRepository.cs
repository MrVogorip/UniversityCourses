using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly GameStoreContext _gameStoreContext;

        public OrderRepository(GameStoreContext gameStoreContext)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
        }

        public ICollection<Order> GetAllByCustomerId(string customerId)
        {
            ICollection<Order> ordersInDb = _gameStoreContext.Set<Order>()
                        .Include(order => order.OrderDetails)
                        .ThenInclude(orderDetails => orderDetails.Game)
                        .Where(order => order.CustomerId == customerId)
                        .ToList();

            return ordersInDb;
        }

        public bool IsExist(string orderId)
        {
            bool orderIsExist = _gameStoreContext.Set<Order>()
                        .Any(order => order.Id == orderId);

            return orderIsExist;
        }

        public override Order GetById(string id)
        {
            Order orderInDb = _gameStoreContext.Set<Order>()
                        .Include(order => order.OrderDetails)
                        .ThenInclude(details => details.Game)
                        .FirstOrDefault(order => order.Id == id);

            return orderInDb;
        }
    }
}
