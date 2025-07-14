namespace tick.Server.Models.views
{
    public class EventView
    {

        public string Name { get; set; } = null!;

        public DateTime EventStart { get; set; }
        public string State { get; set; } = null!;
    }
}
