namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class OsService : IOsService
    {
        private readonly Persistence.IOsService persistenceService;
        private readonly OnDemand.IOsService onDemandService;
        private readonly ILogger logger;

        public OsService(
            Persistence.IOsService persistenceService,
            OnDemand.IOsService onDemandService,
            ILogger logger)
        {
            this.persistenceService = persistenceService;
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Os> GetAsync()
        {
            logger.Info("Application layer -> OsService -> GetAsync");
            return persistenceService.GetAsync();
        }

        public async Task SaveAsync()
        {
            logger.Info("Application layer -> OsService -> SaveAsync");
            var onDemandGpuInfo = await this.onDemandService.GetAsync();
            var persistedGpuInfo = await this.persistenceService.GetAsync();
            if (persistedGpuInfo == null)
            {
                logger.Debug("Os info not set on DB, creating...");
                await this.persistenceService.AddAsync(onDemandGpuInfo);
            }
            else
            {
                logger.Debug("Updating os info on DB...");
                await this.persistenceService.UpdateAsync(onDemandGpuInfo);
            }
        }
    }
}
