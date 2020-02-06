namespace PiControlPanel.Application.Services
{
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;

    public class ControlPanelService : IControlPanelService
    {
        private readonly Infra.IControlPanelService infraService;
        private readonly ILogger logger;

        public ControlPanelService(Infra.IControlPanelService infraService, ILogger logger)
        {
            this.infraService = infraService;
            this.logger = logger;
        }

        public Task<Hardware> GetHardwareAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetHardwareAsync");
            return infraService.GetHardwareAsync(context);
        }
    }
}
