namespace CanteenManager.Core
{
    public class FoodItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<FoodItem> FoodItems { get; set; }
    }
}