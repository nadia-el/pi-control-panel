namespace PiControlPanel.Domain.Models.Hardware
{
    public class Disk
    {
        public string FileSystem { get; set; }

        public string Type { get; set; }

        public int Total { get; set; }

        public int Used { get; set; }

        public int Available { get; set; }
    }
}
