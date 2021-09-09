using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Order GetOrderInBasket(string customerId);

        Order GetOrderById(string orderId);

        void AddGameToOrder(string gameId, string customerId);

        void ConfirmOrder(string orderId);

        void SetStatusPaid(string orderId);

        bool IsNotPaid(string orderId);
    }
}
