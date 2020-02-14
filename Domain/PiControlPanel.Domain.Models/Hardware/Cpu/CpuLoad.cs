namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    public class CpuLoad
    {
        public CpuAverageLoad Average { get; set; }

        public CpuRealTimeLoad RealTime { get; set; }
    }
}
