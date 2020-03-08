namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public interface ICpuService : IBaseService<Cpu>
    {
        Task<CpuTemperature> GetLastTemperatureAsync();

        Task<IEnumerable<CpuTemperature>> GetTemperaturesAsync();

        IObservable<CpuTemperature> GetTemperatureObservable();

        Task<CpuAverageLoad> GetLastAverageLoadAsync();

        Task<IEnumerable<CpuAverageLoad>> GetAverageLoadsAsync();

        Task<CpuRealTimeLoad> GetLastRealTimeLoadAsync();

        Task<IEnumerable<CpuRealTimeLoad>> GetRealTimeLoadsAsync();

        Task<IDictionary<DateTime, double>> GetTotalRealTimeLoadsAsync(
            IEnumerable<DateTime> dateTimes, CancellationToken cancellationToken);

        Task SaveTemperatureAsync();

        Task SaveAverageLoadAsync();

        Task SaveRealTimeLoadAsync();
    }
}
