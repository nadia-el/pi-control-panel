namespace PiControlPanel.Application.BackgroundServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Memory;
    
    public class MemoryWorker : BaseWorker<Memory>
    {
        public MemoryWorker(
            IMemoryService memoryService,
            IConfiguration configuration,
            ILogger logger)
            : base(memoryService, configuration, logger)
        {
        }

        protected override Task SaveRecurring(CancellationToken stoppingToken)
        {
            return ((IMemoryService)this.service).SaveStatusAsync();
        }
    }
}
