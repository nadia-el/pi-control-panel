namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    public class CpuAverageLoad : BaseTimedObject
    {
        public double LastMinute { get; set; }

        public double Last5Minutes { get; set; }

        public double Last15Minutes { get; set; }
    }
}
