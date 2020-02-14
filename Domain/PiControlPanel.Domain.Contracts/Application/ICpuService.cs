namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public interface ICpuService
    {
        Task<Cpu> GetAsync(BusinessContext context);

        Task<double> GetTemperatureAsync(BusinessContext context);

        Task<CpuAverageLoad> GetAverageLoadAsync(BusinessContext context, int cores);

        Task<CpuRealTimeLoad> GetRealTimeLoadAsync(BusinessContext context);

        IObservable<Cpu> GetObservable(BusinessContext context);
    }
}
