namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class ChipsetService : IChipsetService
    {
        private readonly Infra.OnDemand.IChipsetService onDemandService;
        private readonly ILogger logger;

        public ChipsetService(Infra.OnDemand.IChipsetService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Chipset> GetAsync(BusinessContext context)
        {
            logger.Info("Application layer -> ChipsetService -> GetAsync");
            return onDemandService.GetAsync(context);
        }
    }
}
