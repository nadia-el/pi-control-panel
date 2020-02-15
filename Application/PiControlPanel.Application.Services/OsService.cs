namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class OsService : IOsService
    {
        private readonly Infra.OnDemand.IOsService onDemandService;
        private readonly ILogger logger;

        public OsService(Infra.OnDemand.IOsService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Os> GetAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetAsync");
            return onDemandService.GetAsync(context);
        }
    }
}
