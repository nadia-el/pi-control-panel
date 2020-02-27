namespace PiControlPanel.Infrastructure.Persistence.Entities.Cpu
{
    using System.ComponentModel.DataAnnotations;

    public class Cpu
    {
        [Key]
        [StringLength(50, MinimumLength = 3)]
        public string Model { get; set; }

        [Required]
        public int Cores { get; set; }
    }
}
