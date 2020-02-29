namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class GpuService : IGpuService
    {
        private readonly Persistence.IGpuService persistenceService;
        private readonly OnDemand.IGpuService onDemandService;
        private readonly ILogger logger;

        public GpuService(
            Persistence.IGpuService persistenceService,
            OnDemand.IGpuService onDemandService,
            ILogger logger)
        {
            this.persistenceService = persistenceService;
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Gpu> GetAsync()
        {
            logger.Info("Application layer -> GpuService -> GetAsync");
            return persistenceService.GetAsync();
        }

        public async Task SaveAsync()
        {
            logger.Info("Application layer -> GpuService -> SaveAsync");
            var onDemandGpuInfo = await this.onDemandService.GetAsync();
            var persistedGpuInfo = await this.persistenceService.GetAsync();
            if (persistedGpuInfo == null)
            {
                logger.Debug("Gpu info not set on DB, creating...");
                await this.persistenceService.AddAsync(onDemandGpuInfo);
            }
            else
            {
                logger.Debug("Updating gpu info on DB...");
                await this.persistenceService.UpdateAsync(onDemandGpuInfo);
            }
        }
    }
}
