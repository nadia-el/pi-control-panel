namespace PiControlPanel.Domain.Models.Hardware.Memory
{
    public class MemoryStatus : BaseTimedObject
    {
        public int Used { get; set; }

        public int Available { get; set; }
    }
}
