namespace PiControlPanel.Infrastructure.Persistence.Entities.Disk
{
    using System.ComponentModel.DataAnnotations;

    public class Disk : BaseEntity
    {
        [Key]
        [StringLength(50, MinimumLength = 3)]
        public string FileSystem { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        public int Total { get; set; }
    }
}
