namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    using System;

    public class CpuAverageLoad
    {
        public double LastMinute { get; set; }

        public double Last5Minutes { get; set; }

        public double Last15Minutes { get; set; }

        public DateTime DateTime { get; set; }
    }
}
