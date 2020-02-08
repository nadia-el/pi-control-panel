namespace PiControlPanel.Infrastructure.OnDemand.Utils
{
    public class Constants
    {
        public const string TemperatureCommand = "vcgencmd measure_temp";
        public const string ShutdownCommand = "sudo shutdown -h now";
    }
}
