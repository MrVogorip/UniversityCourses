using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Web.Helpers;
using GameStore.Web.Payments.Enums;
using GameStore.Web.Payments.Interfaces;
using GameStore.Web.ViewModel.Order;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IPaymentContext _paymentContext;
        private readonly string _userId;

        public OrderController(
            IOrderService orderService,
            IGameService gameService,
            IPaymentContext paymentContext,
            IMapper mapper)
        {
            _orderService = orderService;
            _gameService = gameService;
            _paymentContext = paymentContext;
            _mapper = mapper;
            _userId = "1";
        }

        [HttpGet]
        [Route(ResourceNames.Urls.Basket)]
        public ActionResult GetBasketInfo()
        {
            var order = _orderService.GetOrderInBasket(_userId);
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        [Route("/game/{gamekey}/buy")]
        public ActionResult AddGameToOrder(string gamekey)
        {
            if (!_gameService.IsExist(gamekey))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var game = _gameService.GetByKeyForOrder(gamekey);
            _orderService.AddGameToOrder(game.Id, _userId);

            return Redirect(ResourceNames.Urls.Basket);
        }

        [Route("/order/{orderId}")]
        public ActionResult SubmitOrder(string orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order.OrderDetails.Count == 0)
            {
                return RedirectToAction(ResourceNames.Methods.BadRequest, ResourceNames.Classes.Error);
            }

            _orderService.ConfirmOrder(order.Id);

            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return View(orderViewModel);
        }

        [HttpGet]
        [Route("/order/{orderId}/pay")]
        public ActionResult PayForOrder(PaymentType method, string orderId)
        {
            if (!_orderService.IsNotPaid(orderId))
            {
                return RedirectToAction(ResourceNames.Methods.BadRequest, ResourceNames.Classes.Error);
            }

            var order = _orderService.GetOrderById(orderId);
            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            _paymentContext.SetPaymentType(method);

            return _paymentContext.RenderPaymentView(orderViewModel);
        }

        [HttpPost]
        public ActionResult ConfirmOrder(PaymentType type, ConfirmOrderViewModel orderViewModel)
        {
            if (!_orderService.IsNotPaid(orderViewModel.Id))
            {
                return RedirectToAction(ResourceNames.Methods.BadRequest, ResourceNames.Classes.Error);
            }

            _paymentContext.SetPaymentType(type);

            return _paymentContext.ConfirmPayment(orderViewModel);
        }
    }
}
