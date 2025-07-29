using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tick.Server.Models
{
    public class Event
    {
        public Event()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idEvent")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime EventStart { get; set; }
        [Required]
        public string State { get; set; } = null!;
        [Column("idPhysicalSeatLayout")]
        public int PhysicalLayoutId { get; set; }
        public PhysicalLayout PhysicalLayout { get; set; } =null!;
    }
}
