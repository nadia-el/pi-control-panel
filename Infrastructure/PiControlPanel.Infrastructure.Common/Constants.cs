namespace PiControlPanel.Infrastructure.Common
{
    public class Constants
    {
        public const string TemperatureCommand = "vcgencmd measure_temp";
        public const string ShutdownCommand = "sudo shutdown -h now";
        public const string CatEtcShadowCommand = "sudo cat /etc/shadow | grep {0}";
        public const string OpenSslPasswdCommand = "openssl passwd -{0} -salt {1} {2}";
    }
}
