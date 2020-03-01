namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using NLog;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Contracts.Util;
    using PiControlPanel.Domain.Models.Hardware;

    public class OsService : BaseService<Os>, IOsService
    {
        public OsService(ILogger logger)
            : base(logger)
        {
        }

        protected override Os GetModel()
        {
            var result = BashCommands.Hostnamectl.Bash();
            logger.Debug($"Result of '{BashCommands.Hostnamectl}' command: '{result}'");
            string[] lines = result.Split(new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            var hostnameInfo = lines.First(l => l.Contains("Static hostname:"));
            var hostname = hostnameInfo.Replace("Static hostname:", string.Empty).Trim();
            logger.Debug($"Hostname: '{hostname}'");

            var osInfo = lines.First(l => l.Contains("Operating System:"));
            var os = osInfo.Replace("Operating System:", string.Empty).Trim();
            logger.Debug($"Operating System Name: '{os}'");

            var kernelInfo = lines.First(l => l.Contains("Kernel:"));
            var kernel = kernelInfo.Replace("Kernel:", string.Empty).Trim();
            logger.Debug($"Kernel: '{kernel}'");

            return new Os()
            {
                Name = os,
                Kernel = kernel,
                Hostname = hostname
            };
        }
    }
}
