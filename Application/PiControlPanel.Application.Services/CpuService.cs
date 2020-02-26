namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
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
            logger.Info("Application layer -> CpuService -> GetAsync");
            return onDemandService.GetAsync(context);
        }

        public Task<double> GetTemperatureAsync(BusinessContext context)
        {
            logger.Info("Application layer -> CpuService -> GetTemperatureAsync");
            return onDemandService.GetTemperatureAsync();
        }

        public Task<CpuAverageLoad> GetAverageLoadAsync(BusinessContext context, int cores)
        {
            logger.Info("Application layer -> CpuService -> GetAverageLoadAsync");
            logger.Debug($"Number of cores for average load calculation is {cores}.");
            return onDemandService.GetAverageLoadAsync(context, cores);
        }

        public Task<CpuRealTimeLoad> GetRealTimeLoadAsync(BusinessContext context)
        {
            logger.Info("Application layer -> CpuService -> GetRealTimeLoadAsync");
            return onDemandService.GetRealTimeLoadAsync(context);
        }

        public IObservable<Cpu> GetObservable(BusinessContext context)
        {
            logger.Info("Application layer -> CpuService -> GetObservable");
            return onDemandService.GetObservable(context);
        }
    }
}
