namespace tick.Server.Models.views
{
    public class ReserveTicketView
    {
        public string OwnerName { get; set; } = null!;
        public int IdEvent { get; set; }
        public int IdSeat { get; set; }
    }
}
