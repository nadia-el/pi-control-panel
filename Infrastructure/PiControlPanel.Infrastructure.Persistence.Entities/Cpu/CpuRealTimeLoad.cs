namespace PiControlPanel.Infrastructure.Persistence.Entities.Cpu
{
    using System.ComponentModel.DataAnnotations;

    public class CpuRealTimeLoad : BaseTimedEntity
    {
        [Required]
        [Range(0, 100)]
        public double Kernel { get; set; }

        [Required]
        [Range(0, 100)]
        public double User { get; set; }
    }
}
