namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class CpuService : ICpuService
    {
        private readonly Persistence.ICpuService persistenceService;
        private readonly OnDemand.ICpuService onDemandService;
        private readonly ILogger logger;

        public CpuService(
            Persistence.ICpuService persistenceService,
            OnDemand.ICpuService onDemandService,
            ILogger logger)
        {
            this.persistenceService = persistenceService;
            this.onDemandService = onDemandService;
            this.logger = logger;
        }

        public Task<Cpu> GetAsync()
        {
            logger.Info("Application layer -> CpuService -> GetAsync");
            return this.persistenceService.GetAsync();
        }

        public async Task<CpuTemperature> GetLastTemperatureAsync()
        {
            logger.Info("Application layer -> CpuService -> GetLastTemperatureAsync");
            return await this.persistenceService.GetLastTemperatureAsync();
        }

        public async Task<IEnumerable<CpuTemperature>> GetTemperaturesAsync()
        {
            logger.Info("Application layer -> CpuService -> GetTemperaturesAsync");
            return await this.persistenceService.GetTemperaturesAsync();
        }

        public IObservable<CpuTemperature> GetTemperatureObservable()
        {
            logger.Info("Application layer -> CpuService -> GetObservable");
            return this.onDemandService.GetTemperatureObservable();
        }

        public Task<CpuAverageLoad> GetAverageLoadAsync(BusinessContext context, int cores)
        {
            logger.Info("Application layer -> CpuService -> GetAverageLoadAsync");
            logger.Debug($"Number of cores for average load calculation is {cores}.");
            return this.onDemandService.GetAverageLoadAsync(context, cores);
        }

        public Task<CpuRealTimeLoad> GetRealTimeLoadAsync(BusinessContext context)
        {
            logger.Info("Application layer -> CpuService -> GetRealTimeLoadAsync");
            return this.onDemandService.GetRealTimeLoadAsync(context);
        }

        public async Task SaveAsync()
        {
            logger.Info("Application layer -> CpuService -> SaveAsync");
            var onDemandCpuInfo = await this.onDemandService.GetAsync();
            var persistedCpuInfo = await this.persistenceService
                .GetAsync(onDemandCpuInfo.Model);
            if (persistedCpuInfo == null)
            {
                logger.Debug("Cpu info not set on DB, creating...");
                await this.persistenceService.AddAsync(onDemandCpuInfo);
            }
            else
            {
                logger.Debug("Updating cpu info on DB...");
                await this.persistenceService.UpdateAsync(onDemandCpuInfo);
            }
        }

        public async Task SaveTemperatureAsync()
        {
            logger.Info("Application layer -> CpuService -> SaveTemperatureAsync");
            var temperature = await this.onDemandService.GetTemperatureAsync();
            
            this.onDemandService.PublishTemperature(temperature);
            await this.persistenceService.AddTemperatureAsync(temperature);
        }
    }
}
