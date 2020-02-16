namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class DiskService : IDiskService
    {
        private readonly Infra.OnDemand.IDiskService onDemandService;
        private readonly ILogger logger;

        public DiskService(Infra.OnDemand.IDiskService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Disk> GetAsync(BusinessContext context)
        {
            logger.Info("Application layer -> DiskService -> GetAsync");
            return onDemandService.GetAsync(context);
        }
    }
}
