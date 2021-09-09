namespace GameStore.Domain.Models
{
    public class OrderDetails : BaseEntity
    {
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public string GameId { get; set; }

        public Game Game { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}
