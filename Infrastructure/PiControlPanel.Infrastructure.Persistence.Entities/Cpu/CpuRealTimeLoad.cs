namespace PiControlPanel.Infrastructure.Persistence.Entities.Cpu
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CpuRealTimeLoad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        [Range(0, 100)]
        public double Kernel { get; set; }

        [Required]
        [Range(0, 100)]
        public double User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}
