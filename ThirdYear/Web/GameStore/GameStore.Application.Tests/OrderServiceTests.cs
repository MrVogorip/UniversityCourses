using System;
using System.Collections.Generic;
using GameStore.Application.Services;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Application.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderService _orderService;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IOrderDetailsRepository> _orderDetailsRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IGameRepository> _gameRepositoryMock;
        private Game _game;
        private Order _order;
        private ICollection<Order> _orders;
        private ICollection<OrderDetails> _orderDetails;
        private string _userIdTest;

        [TestInitialize]
        public void OrderServiceTestsInitialize()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderDetailsRepositoryMock = new Mock<IOrderDetailsRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _orderService = new OrderService(_unitOfWorkMock.Object, _orderRepositoryMock.Object, _orderDetailsRepositoryMock.Object, _gameRepositoryMock.Object);
            _order = new Order
            {
                Id = "1-1-2-2-3",
                CustomerId = "1",
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetails>(),
            };
            _orders = new List<Order> { _order };
            _orderDetails = new List<OrderDetails>
            {
                new OrderDetails
                {
                    GameId = "1",
                    Id = "456",
                },
                new OrderDetails
                {
                    Id = "789",
                },
            };
            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name game",
            };
            _userIdTest = "1";
        }

        [TestMethod]
        public void GetOrderInBasket_ShouldCallRepositoryGetByCustomerId_Always()
        {
            _orderRepositoryMock.Setup(x => x.GetAllByCustomerId(It.IsAny<string>())).Returns(_orders);

            var order = _orderService.GetOrderInBasket(_order.CustomerId);

            Assert.AreEqual(order.Id, _order.Id);
        }

        [TestMethod]
        public void AddGameToOrder_ShouldCallRepositoryGetByCustomerIdAndGetByIdForOrder_InsertWhenIsExist()
        {
            _gameRepositoryMock.Setup(x => x.GetByIdForOrder(It.IsAny<string>())).Returns(_game);
            _orderRepositoryMock.Setup(x => x.GetAllByCustomerId(It.IsAny<string>())).Returns(_orders);
            _orderDetailsRepositoryMock.Setup(x => x.GetAllByOrderId(It.IsAny<string>())).Returns(_orderDetails);

            _orderService.AddGameToOrder(_game.Id, _userIdTest);

            _orderDetailsRepositoryMock.Verify(x => x.Update(It.IsAny<OrderDetails>()), Times.Once);
        }

        [TestMethod]
        public void ConfirmOrder_ShouldCallRepositoryGetByCustomerId_Always()
        {
            _orderRepositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(_order);

            _orderService.ConfirmOrder(_order.Id);

            _orderRepositoryMock.Verify(x => x.Update(_order), Times.Once);
        }

        [TestMethod]
        public void IsNotPaid_ShouldCallRepositoryGetById_Always()
        {
            _orderRepositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(_order);

            var result = _orderService.IsNotPaid(_order.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetStatusPaid_ShouldCallRepositoryGetByIdAndUpdate_Always()
        {
            _orderRepositoryMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(_order);

            _orderService.SetStatusPaid(_order.Id);

            _orderRepositoryMock.Verify(x => x.Update(_order), Times.Once);
        }
    }
}
