namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    public class CpuRealTimeLoad : BaseTimedObject
    {
        public double Kernel { get; set; }

        public double User { get; set; }
    }
}
