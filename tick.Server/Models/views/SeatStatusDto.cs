namespace tick.Server.Models.views
{
    public class SeatStatusDto
    {
        public int SeatId { get; set; }
        public int SeatNumber { get; set; }
        public int Row { get; set; }
        public string SeatStatus { get; set; } = null!; // "full", "busy", "available"
    }
}
