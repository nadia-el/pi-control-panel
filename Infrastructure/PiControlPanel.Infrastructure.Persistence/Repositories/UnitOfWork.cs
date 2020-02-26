namespace PiControlPanel.Infrastructure.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using NLog;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    using PiControlPanel.Infrastructure.Persistence.Entities;
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

        public IRepositoryBase<Chipset> ChipsetRepository => new RepositoryBase<Chipset>(this.dbContext, this.logger);

        #endregion
    }
}
