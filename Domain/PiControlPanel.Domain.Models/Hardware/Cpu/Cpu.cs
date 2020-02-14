namespace PiControlPanel.Domain.Models.Hardware.Cpu
{
    public class Cpu
    {
        public double Temperature { get; set; }

        public int Cores { get; set; }

        public string Model { get; set; }

        public CpuLoad Load { get; set; }
    }
}
