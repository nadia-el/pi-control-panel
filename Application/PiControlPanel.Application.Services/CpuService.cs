namespace PiControlPanel.Application.Services
{
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OnDemand = PiControlPanel.Domain.Contracts.Infrastructure.OnDemand;
    using Persistence = PiControlPanel.Domain.Contracts.Infrastructure.Persistence;

    public class CpuService : BaseService<Cpu>, ICpuService
    {
        private readonly Persistence.Cpu.ICpuTemperatureService persistenceTemperatureService;
        private readonly Persistence.Cpu.ICpuAverageLoadService persistenceAverageLoadService;
        private readonly Persistence.Cpu.ICpuRealTimeLoadService persistenceRealTimeLoadService;

        public CpuService(
            Persistence.Cpu.ICpuService persistenceService,
            Persistence.Cpu.ICpuTemperatureService persistenceTemperatureService,
            Persistence.Cpu.ICpuAverageLoadService persistenceAverageLoadService,
            Persistence.Cpu.ICpuRealTimeLoadService persistenceRealTimeLoadService,
            OnDemand.ICpuService onDemandService,
            ILogger logger)
            : base(persistenceService, onDemandService, logger)
        {
            this.persistenceTemperatureService = persistenceTemperatureService;
            this.persistenceAverageLoadService = persistenceAverageLoadService;
            this.persistenceRealTimeLoadService = persistenceRealTimeLoadService;
        }

        public async Task<CpuTemperature> GetLastTemperatureAsync()
        {
            logger.Info("Application layer -> CpuService -> GetLastTemperatureAsync");
            return await this.persistenceTemperatureService.GetLastAsync();
        }

        public async Task<IEnumerable<CpuTemperature>> GetTemperaturesAsync()
        {
            logger.Info("Application layer -> CpuService -> GetTemperaturesAsync");
            return await this.persistenceTemperatureService.GetAllAsync();
        }

        public IObservable<CpuTemperature> GetTemperatureObservable()
        {
            logger.Info("Application layer -> CpuService -> GetTemperatureObservable");
            return ((OnDemand.ICpuService)this.onDemandService).GetTemperatureObservable();
        }

        public Task<CpuAverageLoad> GetLastAverageLoadAsync()
        {
            logger.Info("Application layer -> CpuService -> GetLastAverageLoadAsync");
            return this.persistenceAverageLoadService.GetLastAsync();
        }

        public async Task<IEnumerable<CpuAverageLoad>> GetAverageLoadsAsync()
        {
            logger.Info("Application layer -> CpuService -> GetAverageLoadsAsync");
            return await this.persistenceAverageLoadService.GetAllAsync();
        }

        public Task<CpuRealTimeLoad> GetLastRealTimeLoadAsync()
        {
            logger.Info("Application layer -> CpuService -> GetLastRealTimeLoadAsync");
            return this.persistenceRealTimeLoadService.GetLastAsync();
        }

        public async Task<IEnumerable<CpuRealTimeLoad>> GetRealTimeLoadsAsync()
        {
            logger.Info("Application layer -> CpuService -> GetRealTimeLoadsAsync");
            return await this.persistenceRealTimeLoadService.GetAllAsync();
        }

        public Task<double> GetTotalRealTimeLoadAsync(CpuRealTimeLoad cpuRealTimeLoad)
        {
            logger.Info("Application layer -> CpuService -> GetTotalRealTimeLoad");
            return Task.FromResult(cpuRealTimeLoad.Kernel + cpuRealTimeLoad.User);
        }

        public async Task SaveTemperatureAsync()
        {
            logger.Info("Application layer -> CpuService -> SaveTemperatureAsync");
            var temperature = await ((OnDemand.ICpuService)this.onDemandService).GetTemperatureAsync();
            
            ((OnDemand.ICpuService)this.onDemandService).PublishTemperature(temperature);
            await this.persistenceTemperatureService.AddAsync(temperature);
        }

        public async Task SaveAverageLoadAsync()
        {
            logger.Info("Application layer -> CpuService -> SaveAverageLoadAsync");
            var cpu = await this.persistenceService.GetAsync();
            if (cpu == null)
            {
                logger.Warn("Cpu info not available yet, can't calculate average load");
                return;
            }

            var averageLoad = await ((OnDemand.ICpuService)this.onDemandService).GetAverageLoadAsync(cpu.Cores);
            await this.persistenceAverageLoadService.AddAsync(averageLoad);
        }

        public async Task SaveRealTimeLoadAsync()
        {
            logger.Info("Application layer -> CpuService -> SaveRealTimeLoadAsync");
            var realTimeLoad = await ((OnDemand.ICpuService)this.onDemandService).GetRealTimeLoadAsync();
            await this.persistenceRealTimeLoadService.AddAsync(realTimeLoad);
        }

        protected async override Task<Cpu> GetPersistedInfoAsync(Cpu onDemandInfo)
        {
            return await ((Persistence.Cpu.ICpuService)this.persistenceService)
                .GetAsync(onDemandInfo.Model);
        }
    }
}
