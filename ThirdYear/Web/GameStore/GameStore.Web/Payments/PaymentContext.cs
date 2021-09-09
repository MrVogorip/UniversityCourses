using System.Collections.Generic;
using System.Linq;
using GameStore.Web.Payments.Enums;
using GameStore.Web.Payments.Interfaces;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Payments
{
    public class PaymentContext : IPaymentContext
    {
        private readonly IEnumerable<IPaymentStrategy> _paymentStrategies;
        private IPaymentStrategy _payment;

        public PaymentContext(IEnumerable<IPaymentStrategy> paymentStrategies)
        {
            _paymentStrategies = paymentStrategies;
        }

        public void SetPaymentType(PaymentType type)
        {
            _payment = _paymentStrategies.First(x => x.Type == type);
        }

        public ActionResult RenderPaymentView(OrderViewModel orderViewModel)
        {
            return _payment.RenderPaymentView(orderViewModel);
        }

        public ActionResult ConfirmPayment(ConfirmOrderViewModel orderViewModel)
        {
            return _payment.ConfirmPayment(orderViewModel);
        }
    }
}
