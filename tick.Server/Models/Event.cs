namespace tick.Server.Models
{
    public class Event
    {
        public Event()
        {
            
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime EventStart { get; set; }
        public string? State { get; set; }
        public int LayoutId { get; set; }

    }
}
