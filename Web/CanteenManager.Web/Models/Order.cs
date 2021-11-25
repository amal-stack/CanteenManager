namespace CanteenManager.Core
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public List<FoodItem> Items { get; set; }

        public DateTime OrderTimestamp { get; set; }

        public Slot Slot { get; set; }
    }
}