namespace PiControlPanel.Domain.Models.Hardware.Disk
{
    public class DiskStatus : BaseTimedObject
    {
        public int Used { get; set; }

        public int Available { get; set; }
    }
}
