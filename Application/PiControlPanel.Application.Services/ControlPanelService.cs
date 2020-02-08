namespace PiControlPanel.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;

    public class ControlPanelService : IControlPanelService
    {
        private readonly Infra.IControlPanelService onDemandService;
        private readonly ILogger logger;

        public ControlPanelService(Infra.IControlPanelService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Hardware> GetHardwareAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetHardwareAsync");
            return onDemandService.GetHardwareAsync(context);
        }

        public IObservable<Hardware> GetHardwareObservable(BusinessContext context)
        {
            logger.Info("Application layer -> GetHardwareObservable");
            return onDemandService.GetHardwareObservable(context);
        }

        public Task<bool> ShutdownAsync(BusinessContext context)
        {
            logger.Info("Application layer -> ShutdownAsync");
            return onDemandService.ShutdownAsync(context);
        }
    }
}
