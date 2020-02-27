namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public interface ICpuService
    {
        Task<Cpu> GetAsync();

        Task<CpuTemperature> GetLastTemperatureAsync();

        Task<IEnumerable<CpuTemperature>> GetTemperaturesAsync();

        IObservable<CpuTemperature> GetTemperatureObservable();

        Task<CpuAverageLoad> GetAverageLoadAsync(BusinessContext context, int cores);

        Task<CpuRealTimeLoad> GetRealTimeLoadAsync(BusinessContext context);

        Task SaveAsync();

        Task SaveTemperatureAsync();
    }
}
