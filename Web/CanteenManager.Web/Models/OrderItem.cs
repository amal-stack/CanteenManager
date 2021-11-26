namespace CanteenManager.Web.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public FoodItem FoodItem { get; set; }

        public int FoodItemId { get; set; }

        public int Quantity { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public FoodItem FoodItem { get; set; }

        public int FoodItemId { get; set; }

        public int Quantity { get; set; }
    }

}