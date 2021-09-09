using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;
using GameStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Infrastructure.Data.Tests
{
    [TestClass]

    public class OrderRepositoryTests
    {
        private DbContextOptions<GameStoreContext> _options;
        private OrderRepository _orderRepository;
        private ICollection<Order> _orders;

        [TestInitialize]
        public void PlatformRepositoryInitialize()
        {
            _options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _orders = new List<Order>
            {
                new Order()
                {
                    Id = "3",
                    CustomerId = "123",
                },
                new Order()
                {
                    Id = "2",
                    CustomerId = "012",
                },
            };
            using var context = new GameStoreContext(_options);
            context.Orders.AddRange(_orders);
            context.SaveChanges();
            _orderRepository = new OrderRepository(new GameStoreContext(_options));
        }

        [TestMethod]
        public void GetById_ShouldCallContext_Always()
        {
            string idTest = _orders.First().Id;

            var order = _orderRepository.GetById(idTest);

            Assert.AreEqual(order.Id, idTest);
        }
    }
}
