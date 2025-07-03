namespace tick.Server.Models
{
    public class Ticket
    {
        public Ticket()
        {
            
        }
        public int Id { get; set; }
        public string? Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? State { get; set; }
        public int EventId { get; set; }
        public int SeatId { get; set; }
    }
}
