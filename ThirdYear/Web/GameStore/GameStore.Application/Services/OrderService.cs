using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Enums;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IGameRepository _gameRepository;

        public OrderService(
            IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            IOrderDetailsRepository orderDetailsRepository,
            IGameRepository gameRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _gameRepository = gameRepository;
        }

        public Order GetOrderInBasket(string customerId)
        {
            var orders = _orderRepository.GetAllByCustomerId(customerId);
            var order = orders.FirstOrDefault(x => x.Status == OrderStatus.Opened);

            if (order is null)
            {
                order = CreateOpened(customerId);
            }

            order.TotalPrice = CalculateTotalPrice(order.OrderDetails);

            return order;
        }

        public Order GetOrderById(string orderId)
        {
            Order order = _orderRepository.GetById(orderId);
            order.TotalPrice = CalculateTotalPrice(order.OrderDetails);

            return order;
        }

        public void AddGameToOrder(string gameId, string customerId)
        {
            var game = _gameRepository.GetByIdForOrder(gameId);
            var order = GetOrderInBasket(customerId);
            var orderDetails = _orderDetailsRepository.GetAllByOrderId(order.Id);
            var gameIsExistInOrder = orderDetails.Any(x => x.GameId == game.Id);

            if (gameIsExistInOrder)
            {
                AddQuantityToOrder(orderDetails, game.Id);
            }
            else
            {
                AddGameToOrder(game, order);
            }
        }

        public void ConfirmOrder(string orderId)
        {
            Order order = _orderRepository.GetById(orderId);
            order.TotalPrice = CalculateTotalPrice(order.OrderDetails);
            order.Status = OrderStatus.Submitted;

            _orderRepository.Update(order);

            _unitOfWork.Commit();
        }

        public bool IsNotPaid(string orderId)
        {
            Order order = _orderRepository.GetById(orderId);

            return order.Status != OrderStatus.Paid;
        }

        public void SetStatusPaid(string orderId)
        {
            Order order = _orderRepository.GetById(orderId);
            order.OrderDate = DateTime.Now;
            order.Status = OrderStatus.Paid;
            order.TotalPrice = CalculateTotalPrice(order.OrderDetails);
            UpdateUnitInStock(order);
            _orderRepository.Update(order);

            _unitOfWork.Commit();
        }

        private void UpdateUnitInStock(Order order)
        {
            foreach (var orderDetails in order.OrderDetails)
            {
                Game game = _gameRepository.GetById(orderDetails.GameId);
                game.UnitsInStock -= orderDetails.Quantity;

                _gameRepository.Update(game);
            }

            _unitOfWork.Commit();
        }

        private Order CreateOpened(string customerId)
        {
            Order order = new Order()
            {
                CustomerId = customerId,
                Status = OrderStatus.Opened,
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetails>(),
            };
            _orderRepository.Insert(order);

            _unitOfWork.Commit();

            return order;
        }

        private void AddQuantityToOrder(ICollection<OrderDetails> orderDetails, string gameId)
        {
            var orderDetail = orderDetails.First(x => x.GameId == gameId);
            orderDetail.Quantity++;
            _orderDetailsRepository.Update(orderDetail);

            _unitOfWork.Commit();
        }

        private void AddGameToOrder(Game game, Order order)
        {
            var newOrderDetails = new OrderDetails
            {
                OrderId = order.Id,
                GameId = game.Id,
                Price = game.Price,
                Discount = game.Discount,
                Quantity = 1,
            };
            _orderDetailsRepository.Insert(newOrderDetails);

            _unitOfWork.Commit();
        }

        private decimal CalculateTotalPrice(ICollection<OrderDetails> orderDetails)
        {
            return orderDetails.Sum(x => ((decimal)(100 - x.Discount) * x.Quantity * x.Price / 100));
        }
    }
}
