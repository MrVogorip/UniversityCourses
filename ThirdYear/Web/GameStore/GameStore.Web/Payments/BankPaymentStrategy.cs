using System.Net.Mime;
using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Helpers;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payments
{
    public class BankPaymentStrategy : PaymentStrategy
    {
        private readonly IInvoiceGenerateService _invoiceGenerator;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public BankPaymentStrategy(IInvoiceGenerateService invoiceGenerator, IOrderService orderService, IMapper mapper)
            : base(PaymentType.Bank)
        {
            _invoiceGenerator = invoiceGenerator;
            _orderService = orderService;
            _mapper = mapper;
        }

        public override ActionResult ConfirmPayment(ConfirmOrderViewModel orderViewModel)
        {
            _orderService.SetStatusPaid(orderViewModel.Id);

            return Redirect(ResourceNames.Urls.Games);
        }

        public override ActionResult RenderPaymentView(OrderViewModel orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);
            var file = _invoiceGenerator.GenerateInPdf(order);
            var fileStreamResult = new FileStreamResult(file, MediaTypeNames.Application.Pdf)
            {
                FileDownloadName = $"Order-{orderViewModel.Id}.pdf",
            };

            return fileStreamResult;
        }
    }
}
