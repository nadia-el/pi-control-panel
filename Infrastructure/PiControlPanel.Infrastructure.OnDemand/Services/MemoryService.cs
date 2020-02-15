namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Common;

    public class MemoryService : IMemoryService
    {
        private readonly ILogger logger;

        public MemoryService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<Memory> GetAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetAsync");
            var memory = this.GetMemory();
            return Task.FromResult(memory);
        }

        private Memory GetMemory()
        {
            var result = Constants.FreeCommand.Bash();
            logger.Debug($"Result of Free from command: '{result}'");
            string[] lines = result.Split(new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
            var memoryInfo = lines.First(l => l.StartsWith("Mem:"));
            var regex = new Regex(@"^Mem:\s*(?<total>\d*)\s*(?<used>\d*)\s*(?<free>\d*)\s*.*$");
            var groups = regex.Match(memoryInfo).Groups;
            var total = int.Parse(groups["total"].Value);
            var used = int.Parse(groups["used"].Value);
            var free = int.Parse(groups["free"].Value);
            logger.Debug($"Total memory: '{total}'KB, Used memory: '{used}'KB, Free memory: '{free}'KB");
            return new Memory()
            {
                Total = total,
                Used = used,
                Available = free
            };
        }
    }
}
