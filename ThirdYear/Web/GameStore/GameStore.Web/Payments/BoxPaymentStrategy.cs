using GameStore.Domain.Interfaces.Services;
using GameStore.Web.Helpers;
using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payments
{
    public class BoxPaymentStrategy : PaymentStrategy
    {
        private readonly IOrderService _orderService;

        public BoxPaymentStrategy(IOrderService orderService)
            : base(PaymentType.Box)
        {
            _orderService = orderService;
        }

        public override ActionResult ConfirmPayment(ConfirmOrderViewModel orderViewModel)
        {
            _orderService.SetStatusPaid(orderViewModel.Id);

            return Redirect(ResourceNames.Urls.Games);
        }

        public override ActionResult RenderPaymentView(OrderViewModel orderViewModel)
        {
            return View(ResourceNames.PaymentTypes.Box);
        }
    }
}
