namespace PiControlPanel.Domain.Models.Hardware.Memory
{
    using System;

    public class MemoryStatus
    {
        public int Used { get; set; }

        public int Available { get; set; }

        public DateTime DateTime { get; set; }
    }
}
