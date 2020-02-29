namespace PiControlPanel.Domain.Models.Hardware.Disk
{
    using System;

    public class DiskStatus
    {
        public int Used { get; set; }

        public int Available { get; set; }

        public DateTime DateTime { get; set; }
    }
}
