using GameStore.Web.ViewModel.Game;

namespace GameStore.Web.ViewModel.Order
{
    public class OrderDetailsViewModel
    {
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public string GameId { get; set; }

        public GameViewModel Game { get; set; }

        public string OrderId { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
