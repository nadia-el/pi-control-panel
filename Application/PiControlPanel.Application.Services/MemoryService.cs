namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Memory;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class MemoryService : BaseService<Memory>, IMemoryService
    {
        private readonly Persistence.Memory.IMemoryStatusService persistenceStatusService;

        public MemoryService(
            Persistence.Memory.IMemoryService persistenceService,
            Persistence.Memory.IMemoryStatusService persistenceStatusService,
            OnDemand.IMemoryService onDemandService,
            ILogger logger)
            : base(persistenceService, onDemandService, logger)
        {
            this.persistenceStatusService = persistenceStatusService;
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

        public async Task SaveStatusAsync()
        {
            logger.Info("Application layer -> MemoryService -> SaveStatusAsync");
            var status = await ((OnDemand.IMemoryService)this.onDemandService).GetStatusAsync();
            await this.persistenceStatusService.AddAsync(status);
        }
    }
}
