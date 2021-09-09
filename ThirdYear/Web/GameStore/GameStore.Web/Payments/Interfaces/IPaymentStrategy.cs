using GameStore.Web.Payments.Enums;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payments.Interfaces
{
    public interface IPaymentStrategy
    {
        PaymentType Type { get; }

        ActionResult RenderPaymentView(OrderViewModel orderViewModel);

        ActionResult ConfirmPayment(ConfirmOrderViewModel orderViewModel);
    }
}
