using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Web.Helpers;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payments
{
    public class VisaPaymentStrategy : PaymentStrategy
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public VisaPaymentStrategy(IOrderService orderService, IMapper mapper)
            : base(PaymentType.Visa)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public override ActionResult ConfirmPayment(ConfirmOrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ResourceNames.PaymentTypes.Visa, orderViewModel);
            }

            _orderService.SetStatusPaid(orderViewModel.Id);

            return View(ResourceNames.NameViews.PaymentSuccessful);
        }

        public override ActionResult RenderPaymentView(OrderViewModel orderViewModel)
        {
            var orderForPayViewModel = _mapper.Map<ConfirmOrderViewModel>(orderViewModel);

            return View(ResourceNames.PaymentTypes.Visa, orderForPayViewModel);
        }
    }
}
