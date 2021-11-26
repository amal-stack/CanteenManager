using CanteenManager.Web.Models.Users;

namespace CanteenManager.Web.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public Customer Customer { get; set; }

        public HashSet<OrderItem> Items { get; set; } 
    }
}