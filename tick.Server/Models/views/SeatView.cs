namespace tick.Server.Models.views
{
    public class SeatView
    {
        public int Row { get; set; }
        public int Seatnumber { get; set; }
        public int? LayoutId { get; set; }
        public string Avaibility { get; set; } = null!;
    }
}
