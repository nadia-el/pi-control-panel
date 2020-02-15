namespace PiControlPanel.Infrastructure.OnDemand.Services
{
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Common;

    public class GpuService : IGpuService
    {
        private readonly ILogger logger;

        public GpuService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<Gpu> GetAsync(BusinessContext context)
        {
            logger.Info("Infra layer -> GetAsync");
            var gpu = this.GetGpu();
            return Task.FromResult(gpu);
        }

        private Gpu GetGpu()
        {
            var result = Constants.GetMemGpuCommand.Bash();
            logger.Debug($"Result of GetMemGpu from command: '{result}'");
            string gpu = result.Replace("gpu=", string.Empty).Replace("M", string.Empty);
            logger.Debug($"Gpu memory: '{gpu}'MB");
            return new Gpu()
            {
                Memory = int.Parse(gpu)
            };
        }
    }
}
