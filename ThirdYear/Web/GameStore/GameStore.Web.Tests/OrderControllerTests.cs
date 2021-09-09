using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Controllers;
using GameStore.Web.Payments.Enums;
using GameStore.Web.Payments.Interfaces;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        private OrderController _orderController;
        private Mock<IOrderService> _orderServiceMock;
        private Mock<IGameService> _gameServiceMock;
        private Mock<IPaymentContext> _paymentContextMock;
        private Mock<IMapper> _mapperMock;
        private Game _game;
        private Order _order;
        private OrderViewModel _orderViewModel;
        private ConfirmOrderViewModel _confirmOrderViewModel;

        [TestInitialize]
        public void OrderControllerTestsInitialize()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _gameServiceMock = new Mock<IGameService>();
            _paymentContextMock = new Mock<IPaymentContext>();
            _mapperMock = new Mock<IMapper>();
            _orderController = new OrderController(_orderServiceMock.Object, _gameServiceMock.Object, _paymentContextMock.Object, _mapperMock.Object);
            _order = new Order
            {
                Id = "123",
                CustomerId = "1",
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetails>(),
            };
            _orderViewModel = new OrderViewModel
            {
                Id = "123",
                CustomerId = "1",
                OrderDate = DateTime.Now,
            };
            _confirmOrderViewModel = new ConfirmOrderViewModel
            {
                Id = "123",
                CustomerId = "1",
                CardHoldersName = "test 1",
                CardNumber = "11111111111111111111",
                CardVerificationValue = 111,
                ExpirationDate = "21/2114",
            };
            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name",
            };
        }

        [TestMethod]
        public void GetOrder_ShouldCallOrderServiceGetByCustomerId_Always()
        {
            _orderServiceMock.Setup(x => x.GetOrderInBasket(It.IsAny<string>())).Returns(_order);
            _mapperMock.Setup(x => x.Map<OrderViewModel>(It.IsAny<Order>())).Returns(_orderViewModel);

            _orderController.GetBasketInfo();

            _orderServiceMock.Verify(x => x.GetOrderInBasket(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void AddGameToOrder_ShouldCallOrderServiceAddGameToCustomer_WhenIsGameExist()
        {
            _gameServiceMock.Setup(x => x.GetByKeyForOrder(It.IsAny<string>())).Returns(_game);
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(true);

            _orderController.AddGameToOrder(_game.Key);

            _orderServiceMock.Verify(x => x.AddGameToOrder(_game.Id, It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void AddGameToOrder_ShouldReturnNotFoundResult_WhenIsGameNotExist()
        {
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _orderController.AddGameToOrder(_game.Key);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void SubmitOrder_ShouldReturnBadRequestResult_WhenOrderDetailsCountIsZero()
        {
            _orderServiceMock.Setup(x => x.GetOrderById(It.IsAny<string>())).Returns(_order);

            var result = _orderController.SubmitOrder(_order.Id);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void PayOrder_ShouldMapModelAndReturnView_Always()
        {
            _orderServiceMock.Setup(x => x.GetOrderById(It.IsAny<string>())).Returns(_order);
            _mapperMock.Setup(x => x.Map<OrderViewModel>(It.IsAny<Order>())).Returns(_orderViewModel);
            _paymentContextMock.Setup(x => x.RenderPaymentView(_orderViewModel)).Returns(new ViewResult());

            var result = _orderController.PayForOrder(PaymentType.Bank, _order.Id);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void PayVisaOrder_ShouldMapModelAndRedirect_Always()
        {
            _paymentContextMock.Setup(x => x.ConfirmPayment(_confirmOrderViewModel)).Returns(new ViewResult());

            var result = _orderController.ConfirmOrder(PaymentType.Visa, _confirmOrderViewModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
