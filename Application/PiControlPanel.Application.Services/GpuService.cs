namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class GpuService : IGpuService
    {
        private readonly Infra.OnDemand.IGpuService onDemandService;
        private readonly ILogger logger;

        public GpuService(Infra.OnDemand.IGpuService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Gpu> GetAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetAsync");
            return onDemandService.GetAsync(context);
        }
    }
}
