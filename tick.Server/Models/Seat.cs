using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tick.Server.Models
{
    public class Seat
    {
        public Seat()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idSeat")]
        public int Id { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Seatnumber { get; set; }
        [Column("idPhysicalLayout")]
        public int? PhysicalLayoutId { get; set; }
        public PhysicalLayout PhysicalLayout { get; set; } = null!;
    }
}
