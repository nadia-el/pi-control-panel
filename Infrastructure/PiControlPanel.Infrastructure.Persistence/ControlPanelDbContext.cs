namespace PiControlPanel.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using PiControlPanel.Infrastructure.Persistence.Entities;

    public class ControlPanelDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ControlPanelDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<Chipset> Chipset { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
    }
}
