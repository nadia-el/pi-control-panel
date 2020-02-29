namespace PiControlPanel.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ControlPanelDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ControlPanelDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<Entities.Chipset> Chipset { get; set; }

        public DbSet<Entities.Cpu.Cpu> Cpu { get; set; }

        public DbSet<Entities.Cpu.CpuTemperature> CpuTemperature { get; set; }

        public DbSet<Entities.Cpu.CpuAverageLoad> CpuAverageLoad { get; set; }

        public DbSet<Entities.Cpu.CpuRealTimeLoad> CpuRealTimeLoad { get; set; }

        public DbSet<Entities.Gpu> Gpu { get; set; }

        public DbSet<Entities.Os> Os { get; set; }

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
