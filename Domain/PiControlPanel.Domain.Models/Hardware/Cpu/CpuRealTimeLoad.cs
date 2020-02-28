namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    using System;

    public class CpuRealTimeLoad
    {
        public double Kernel { get; set; }

        public double User { get; set; }

        public DateTime DateTime { get; set; }
    }
}
