using System;
using System.Collections.Generic;
using System.IO;
using GameStore.Application.Services;
using GameStore.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Application.Tests
{
    [TestClass]
    public class InvoiceGeneratorTest
    {
        private InvoiceGenerateService _invoiceGenerator;
        private Order _order;

        [TestInitialize]
        public void OrderServiceTestsInitialize()
        {
            _invoiceGenerator = new InvoiceGenerateService();
            _order = new Order
            {
                Id = "1-1-2-2-3",
                CustomerId = "1",
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetails>(),
            };
        }

        [TestMethod]
        public void GeneratePdfForOrder_ShouldReturnMemoryStream_Always()
        {
            var file = _invoiceGenerator.GenerateInPdf(_order);

            Assert.IsInstanceOfType(file, typeof(MemoryStream));
        }
    }
}
