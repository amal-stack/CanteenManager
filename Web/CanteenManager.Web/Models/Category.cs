namespace CanteenManager.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<FoodItem> FoodItems { get; set; }
    }
}