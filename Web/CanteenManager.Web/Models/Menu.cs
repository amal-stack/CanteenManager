namespace CanteenManager.Core
{
    public class Menu
    {
        public int Id { get; set; }

        public List<FoodItem> Items { get; set; }
    }



    public class DashboardModel
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

    }
}