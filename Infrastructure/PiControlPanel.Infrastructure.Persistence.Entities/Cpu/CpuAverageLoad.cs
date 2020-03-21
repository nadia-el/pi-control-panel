namespace PiControlPanel.Infrastructure.Persistence.Entities.Cpu
{
    using System.ComponentModel.DataAnnotations;

    public class CpuAverageLoad : BaseTimedEntity
    {
        [Required]
        [Range(0, int.MaxValue)]
        public double LastMinute { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Last5Minutes { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Last15Minutes { get; set; }
    }
}
