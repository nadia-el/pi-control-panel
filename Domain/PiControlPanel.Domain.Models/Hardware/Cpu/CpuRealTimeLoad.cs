namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    public class CpuRealTimeLoad
    {
        public double Kernel { get; set; }

        public double User { get; set; }

        public double Total { get { return Kernel + User; } }
    }
}
