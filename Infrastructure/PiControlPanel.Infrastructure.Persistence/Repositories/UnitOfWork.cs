namespace PiControlPanel.Infrastructure.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using NLog;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ControlPanelDbContext dbContext;
        private readonly ILogger logger;

        public UnitOfWork(IConfiguration configuration, ILogger logger)
        {
            this.dbContext = new ControlPanelDbContext(configuration);
            this.logger = logger;
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public void RejectChanges()
        {
            var changedEntries = dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        #region Repositories

        public IRepositoryBase<Entities.Chipset> ChipsetRepository => new RepositoryBase<Entities.Chipset>(this.dbContext, this.logger);

        public IRepositoryBase<Entities.Cpu.Cpu> CpuRepository => new RepositoryBase<Entities.Cpu.Cpu>(this.dbContext, this.logger);

        public IRepositoryBase<Entities.Cpu.CpuTemperature> CpuTemperatureRepository => new RepositoryBase<Entities.Cpu.CpuTemperature>(this.dbContext, this.logger);

        public IRepositoryBase<Entities.Cpu.CpuAverageLoad> CpuAverageLoadRepository => new RepositoryBase<Entities.Cpu.CpuAverageLoad>(this.dbContext, this.logger);

        public IRepositoryBase<Entities.Cpu.CpuRealTimeLoad> CpuRealTimeLoadRepository => new RepositoryBase<Entities.Cpu.CpuRealTimeLoad>(this.dbContext, this.logger);

        public IRepositoryBase<Entities.Gpu> GpuRepository => new RepositoryBase<Entities.Gpu>(this.dbContext, this.logger);

        public IRepositoryBase<Entities.Os> OsRepository => new RepositoryBase<Entities.Os>(this.dbContext, this.logger);

        #endregion
    }
}
