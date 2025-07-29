using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tick.Server.Models
{
    public class Ticket
    {
        public Ticket()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idTicket")]
        public int Id { get; set; }
        [Required]
        [Column("OwnerName")]
        public string Owner { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string State { get; set; } = null!;
        [Required]
        [Column("idEvent")]
        public int EventId { get; set; }
        [Required]
        [Column("idSeat")]
        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;
        public Event Event { get; set; } = null!;
    }
}
