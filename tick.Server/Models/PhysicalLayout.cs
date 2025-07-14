using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tick.Server.Models
{
    public class PhysicalLayout
    {
        public PhysicalLayout()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idPhysicalSeatLayout")]
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; } = null!;

    }
}
