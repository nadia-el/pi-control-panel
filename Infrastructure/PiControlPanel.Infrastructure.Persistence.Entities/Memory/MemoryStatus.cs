namespace PiControlPanel.Infrastructure.Persistence.Entities.Memory
{
    using System.ComponentModel.DataAnnotations;

    public class MemoryStatus : BaseTimedEntity
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Used { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Available { get; set; }
    }
}
