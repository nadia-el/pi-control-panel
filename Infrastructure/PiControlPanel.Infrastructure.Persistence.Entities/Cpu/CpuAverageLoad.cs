namespace PiControlPanel.Infrastructure.Persistence.Entities.Cpu
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CpuAverageLoad : BaseTimedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        [Range(0, 100)]
        public double LastMinute { get; set; }

        [Required]
        [Range(0, 100)]
        public double Last5Minutes { get; set; }

        [Required]
        [Range(0, 100)]
        public double Last15Minutes { get; set; }
    }
}
