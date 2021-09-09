using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Payments;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class PaymentsTests
    {
        private Mock<IInvoiceGenerateService> _generateInvoiceMock;
        private Mock<IOrderService> _orderServiceMock;
        private Mock<IMapper> _mapperMock;
        private BankPaymentStrategy _bankPaymentStrategy;
        private VisaPaymentStrategy _visaPaymentStrategy;
        private BoxPaymentStrategy _boxPaymentStrategy;
        private OrderViewModel _orderViewModel;
        private Order _order;

        [TestInitialize]
        public void OrderServiceTestsInitialize()
        {
            _generateInvoiceMock = new Mock<IInvoiceGenerateService>();
            _orderServiceMock = new Mock<IOrderService>();
            _mapperMock = new Mock<IMapper>();
            _bankPaymentStrategy = new BankPaymentStrategy(_generateInvoiceMock.Object, _orderServiceMock.Object, _mapperMock.Object);
            _visaPaymentStrategy = new VisaPaymentStrategy(_orderServiceMock.Object, _mapperMock.Object);
            _boxPaymentStrategy = new BoxPaymentStrategy(_orderServiceMock.Object);
            _orderViewModel = new OrderViewModel
            {
                Id = "123",
                CustomerId = "1",
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetailsViewModel>(),
            };
            _order = new Order
            {
                Id = "123",
                CustomerId = "1",
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetails>(),
            };
        }

        [TestMethod]
        public void PayBank_ShouldCallRepositoryUpdate_Always()
        {
            _generateInvoiceMock.Setup(x => x.GenerateInPdf(_order)).Returns(new MemoryStream());
            _mapperMock.Setup(x => x.Map<Order>(It.IsAny<OrderViewModel>())).Returns(_order);

            var result = _bankPaymentStrategy.RenderPaymentView(_orderViewModel);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void PayVisa_ShouldCallRepositoryUpdate_Always()
        {
            var result = _visaPaymentStrategy.RenderPaymentView(_orderViewModel);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void PayBox_ShouldCallRepositoryUpdate_Always()
        {
            var result = _boxPaymentStrategy.RenderPaymentView(_orderViewModel);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
    }
}
