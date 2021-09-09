using GameStore.Web.Payments.Enums;
using GameStore.Web.Payments.Interfaces;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payments
{
    public abstract class PaymentStrategy : Controller, IPaymentStrategy
    {
        public PaymentStrategy(PaymentType type)
        {
            Type = type;
        }

        public PaymentType Type { get; }

        public abstract ActionResult ConfirmPayment(ConfirmOrderViewModel orderViewModel);

        public abstract ActionResult RenderPaymentView(OrderViewModel orderViewModel);
    }
}
