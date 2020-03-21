namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Domain.Models.Paging;

    public interface ICpuService : IBaseService<Cpu>
    {
        Task<CpuTemperature> GetLastTemperatureAsync();

        Task<PagingOutput<CpuTemperature>> GetTemperaturesAsync(PagingInput pagingInput);

        IObservable<CpuTemperature> GetTemperatureObservable();

        Task<CpuAverageLoad> GetLastAverageLoadAsync();

        Task<PagingOutput<CpuAverageLoad>> GetAverageLoadsAsync(PagingInput pagingInput);

        Task<CpuRealTimeLoad> GetLastRealTimeLoadAsync();

        Task<PagingOutput<CpuRealTimeLoad>> GetRealTimeLoadsAsync(PagingInput pagingInput);

        Task<IDictionary<DateTime, double>> GetTotalRealTimeLoadsAsync(
            IEnumerable<DateTime> dateTimes, CancellationToken cancellationToken);

        Task SaveTemperatureAsync();

        Task SaveAverageLoadAsync();

        Task SaveRealTimeLoadAsync();
    }
}
