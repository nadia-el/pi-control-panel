namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Memory;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class MemoryService : IMemoryService
    {
        private readonly Persistence.Memory.IMemoryService persistenceService;
        private readonly Persistence.Memory.IMemoryStatusService persistenceStatusService;
        private readonly OnDemand.IMemoryService onDemandService;
        private readonly ILogger logger;

        public MemoryService(
            Persistence.Memory.IMemoryService persistenceService,
            Persistence.Memory.IMemoryStatusService persistenceStatusService,
            OnDemand.IMemoryService onDemandService,
            ILogger logger)
        {
            this.persistenceService = persistenceService;
            this.persistenceStatusService = persistenceStatusService;
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Memory> GetAsync()
        {
            logger.Info("Application layer -> MemoryService -> GetAsync");
            return persistenceService.GetAsync();
        }

        public async Task<MemoryStatus> GetLastStatusAsync()
        {
            logger.Info("Application layer -> MemoryService -> GetLastStatusAsync");
            return await this.persistenceStatusService.GetLastAsync();
        }

        public async Task<IEnumerable<MemoryStatus>> GetStatusesAsync()
        {
            logger.Info("Application layer -> MemoryService -> GetStatusesAsync");
            return await this.persistenceStatusService.GetAllAsync();
        }

        public async Task SaveAsync()
        {
            logger.Info("Application layer -> MemoryService -> SaveAsync");
            var onDemandMemoryInfo = await this.onDemandService.GetAsync();
            var persistedMemoryInfo = await this.persistenceService.GetAsync();
            if (persistedMemoryInfo == null)
            {
                logger.Debug("Memory info not set on DB, creating...");
                await this.persistenceService.AddAsync(onDemandMemoryInfo);
            }
            else
            {
                logger.Debug("Updating memory info on DB...");
                await this.persistenceService.UpdateAsync(onDemandMemoryInfo);
            }
        }

        public async Task SaveStatusAsync()
        {
            logger.Info("Application layer -> MemoryService -> SaveStatusAsync");
            var status = await this.onDemandService.GetStatusAsync();
            await this.persistenceStatusService.AddAsync(status);
        }
    }
}
