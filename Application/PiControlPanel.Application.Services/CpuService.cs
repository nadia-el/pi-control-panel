namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;
    using System;
    using System.Threading.Tasks;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class CpuService : ICpuService
    {
        private readonly Infra.OnDemand.ICpuService onDemandService;
        private readonly ILogger logger;

        public CpuService(Infra.OnDemand.ICpuService onDemandService, ILogger logger)
        {
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Cpu> GetAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetAsync");
            return onDemandService.GetAsync(context);
        }

        public Task<double> GetTemperatureAsync(BusinessContext context)
        {
            logger.Info("Application layer -> GetTemperatureAsync");
            return onDemandService.GetTemperatureAsync(context);
        }

        public IObservable<Cpu> GetCpuObservable(BusinessContext context)
        {
            logger.Info("Application layer -> GetCpuObservable");
            return onDemandService.GetCpuObservable(context);
        }
    }
}
