namespace PiControlPanel.Infrastructure.Persistence.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseTimedEntity : BaseEntity
    {

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}
