namespace tick.Server.Models
{
    public class Seat
    {
        public Seat()
        {
            
        }

        public int Id { get; set; }
        public int Row { get; set; }
        public int Seatnumber { get; set; }
        public int LayoutId { get; set; }
    }
}
