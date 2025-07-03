namespace tick.Server.Models
{
    public class Seatlock
    {
        public Seatlock()
        {
            
        }
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ValidUntil { get; set; }
        public int Lockcode { get; set; }
        public int EventId { get; set; }
        public int SeatId { get; set; }
    }
}
