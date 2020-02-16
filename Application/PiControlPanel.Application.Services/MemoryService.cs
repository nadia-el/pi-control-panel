namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class MemoryService : IMemoryService
    {
        private readonly Infra.OnDemand.IMemoryService onDemandService;
        private readonly ILogger logger;

        public MemoryService(Infra.OnDemand.IMemoryService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Memory> GetAsync(BusinessContext context)
        {
            logger.Info("Application layer -> MemoryService -> GetAsync");
            return onDemandService.GetAsync(context);
        }
    }
}
