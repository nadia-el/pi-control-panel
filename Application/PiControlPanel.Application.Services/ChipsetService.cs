namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class ChipsetService : IChipsetService
    {
        private readonly Persistence.IChipsetService persistenceService;
        private readonly OnDemand.IChipsetService onDemandService;
        private readonly ILogger logger;

        public ChipsetService(
            Persistence.IChipsetService persistenceService,
            OnDemand.IChipsetService onDemandService,
            ILogger logger)
        {
            this.persistenceService = persistenceService;
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Chipset> GetAsync()
        {
            logger.Info("Application layer -> ChipsetService -> GetAsync");
            return this.persistenceService.GetAsync();
        }

        public async Task SaveAsync()
        {
            logger.Info("Application layer -> ChipsetService -> SaveAsync");
            var onDemandChipsetInfo = await this.onDemandService.GetAsync();
            var persistedChipsetInfo = await this.persistenceService
                .GetAsync(onDemandChipsetInfo.Serial);
            if (persistedChipsetInfo == null)
            {
                logger.Debug("Chipset info not set on DB, creating...");
                await this.persistenceService.AddAsync(onDemandChipsetInfo);
            }
            else
            {
                logger.Debug("Updating chipset info on DB...");
                await this.persistenceService.UpdateAsync(onDemandChipsetInfo);
            }
        }
    }
}
