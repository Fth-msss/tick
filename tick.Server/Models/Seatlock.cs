using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tick.Server.Models
{
    public class Seatlock
    {
        public Seatlock()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idSeatLock")]
        public int Id { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }
        [Required]
        [Column("idUser")]
        public int UserId { get; set; }
        [Required]
        [Column("idEvent")]
        public int EventId { get; set; }
        [Required]
        [Column("idSeat")]
        public int SeatId { get; set; }
        public Event Event { get; set; } = null!;
        public Seat Seat { get; set; } = null!;
    }
}
