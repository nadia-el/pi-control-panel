namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public interface ICpuService
    {
        Task<Cpu> GetAsync();

        Task<CpuTemperature> GetTemperatureAsync();

        IObservable<CpuTemperature> GetTemperatureObservable();

        Task<CpuAverageLoad> GetAverageLoadAsync(BusinessContext context, int cores);

        Task<CpuRealTimeLoad> GetRealTimeLoadAsync(BusinessContext context);

        void PublishTemperature(CpuTemperature temperature);
    }
}
