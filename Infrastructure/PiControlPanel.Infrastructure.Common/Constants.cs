namespace PiControlPanel.Infrastructure.Common
{
    public class Constants
    {
        public const string MeasureTempCommand = "vcgencmd measure_temp";
        public const string ShutdownCommand = "sudo shutdown -h now";
        public const string CatEtcShadowCommand = "sudo cat /etc/shadow | grep {0}";
        public const string OpenSslPasswdCommand = "openssl passwd -{0} -salt {1} {2}";
        public const string CatProcCpuInfoCommand = "cat /proc/cpuinfo";
        public const string DfCommand = "df -T";
        public const string FreeCommand = "free --mega";
        public const string GetMemGpuCommand = "vcgencmd get_mem gpu";
        public const string TopCommand = "top -bc -n 1";
    }
}
