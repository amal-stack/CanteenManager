namespace CanteenManager.Core
{
    public class Slot
    {
        public int Id { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int OrderLimit { get; set; }

        public List<Order> Orders { get; set; }
    }
}