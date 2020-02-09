namespace PiControlPanel.Domain.Models.Hardware
{
    public class Cpu
    {
        public double Temperature { get; set; }

        public int Cores { get; set; }

        public string Model { get; set; }

        public double LoadAverageLastMinute { get; set; }

        public double LoadAverageLast5Minutes { get; set; }

        public double LoadAverageLast15Minutes { get; set; }
    }
}
