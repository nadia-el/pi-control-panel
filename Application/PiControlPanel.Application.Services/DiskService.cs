namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class DiskService : IDiskService
    {
        private readonly Persistence.Disk.IDiskService persistenceService;
        private readonly Persistence.Disk.IDiskStatusService persistenceStatusService;
        private readonly OnDemand.IDiskService onDemandService;
        private readonly ILogger logger;

        public DiskService(
            Persistence.Disk.IDiskService persistenceService,
            Persistence.Disk.IDiskStatusService persistenceStatusService,
            OnDemand.IDiskService onDemandService,
            ILogger logger)
        {
            this.persistenceService = persistenceService;
            this.persistenceStatusService = persistenceStatusService;
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Disk> GetAsync()
        {
            logger.Info("Application layer -> DiskService -> GetAsync");
            return persistenceService.GetAsync();
        }

        public async Task<DiskStatus> GetLastStatusAsync()
        {
            logger.Info("Application layer -> DiskService -> GetLastStatusAsync");
            return await this.persistenceStatusService.GetLastAsync();
        }

        public async Task<IEnumerable<DiskStatus>> GetStatusesAsync()
        {
            logger.Info("Application layer -> DiskService -> GetStatusesAsync");
            return await this.persistenceStatusService.GetAllAsync();
        }

        public async Task SaveAsync()
        {
            logger.Info("Application layer -> DiskService -> SaveAsync");
            var onDemandDiskInfo = await this.onDemandService.GetAsync();
            var persistedDiskInfo = await this.persistenceService
                .GetAsync(onDemandDiskInfo.FileSystem);
            if (persistedDiskInfo == null)
            {
                logger.Debug("Disk info not set on DB, creating...");
                await this.persistenceService.AddAsync(onDemandDiskInfo);
            }
            else
            {
                logger.Debug("Updating disk info on DB...");
                await this.persistenceService.UpdateAsync(onDemandDiskInfo);
            }
        }

        public async Task SaveStatusAsync()
        {
            logger.Info("Application layer -> DiskService -> SaveStatusAsync");
            var status = await this.onDemandService.GetStatusAsync();
            await this.persistenceStatusService.AddAsync(status);
        }
    }
}
