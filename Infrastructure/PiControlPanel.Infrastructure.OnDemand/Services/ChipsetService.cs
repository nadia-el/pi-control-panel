namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Contracts.Util;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;

    public class ChipsetService : IChipsetService
    {
        private readonly ILogger logger;

        public ChipsetService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<Chipset> GetAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> ChipsetService -> GetAsync");
            var chipset = this.GetChipset();
            return Task.FromResult(chipset);
        }

        private Chipset GetChipset()
        {
            var result = BashCommands.CatProcCpuInfo.Bash();
            logger.Debug($"Result of '{BashCommands.CatProcCpuInfo}' command: '{result}'");
            string[] lines = result.Split(new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);
            var version = lines.Last(line => line.StartsWith("Hardware"))
                .Split(':')[1].Trim();
            logger.Debug($"Chipset version: '{version}'");
            var revision = lines.Last(line => line.StartsWith("Revision"))
                .Split(':')[1].Trim();
            logger.Debug($"Chipset revision: '{revision}'");
            var serial = lines.Last(line => line.StartsWith("Serial"))
                .Split(':')[1].Trim();
            logger.Debug($"Chipset serial: '{serial}'");
            var model = lines.Last(line => line.StartsWith("Model"))
                .Split(':')[1].Trim();
            logger.Debug($"Chipset model: '{model}'");
            return new Chipset()
            {
                Version = version,
                Revision = revision,
                Serial = serial,
                Model = model
            };
        }
    }
}
