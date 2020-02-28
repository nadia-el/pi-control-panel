namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public interface ICpuService
    {
        Task<Cpu> GetAsync();

        Task<CpuTemperature> GetTemperatureAsync();

        IObservable<CpuTemperature> GetTemperatureObservable();

        Task<CpuAverageLoad> GetAverageLoadAsync(int cores);

        Task<CpuRealTimeLoad> GetRealTimeLoadAsync();

        void PublishTemperature(CpuTemperature temperature);
    }
}
