namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Contracts.Util;
    using PiControlPanel.Domain.Models.Hardware;

    public class GpuService : BaseService<Gpu>, IGpuService
    {
        public GpuService(ILogger logger)
            : base(logger)
        {
        }

        protected override Gpu GetModel()
        {
            var result = BashCommands.GetMemGpu.Bash();
            logger.Debug($"Result of '{BashCommands.GetMemGpu}' command: '{result}'");
            string gpu = result.Replace("gpu=", string.Empty).Replace("M", string.Empty);
            logger.Debug($"Gpu memory: '{gpu}'MB");
            return new Gpu()
            {
                Memory = int.Parse(gpu)
            };
        }
    }
}
