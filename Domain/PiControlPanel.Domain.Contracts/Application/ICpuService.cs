namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public interface ICpuService
    {
        Task<Cpu> GetAsync();

        Task<CpuTemperature> GetLastTemperatureAsync();

        Task<IEnumerable<CpuTemperature>> GetTemperaturesAsync();

        IObservable<CpuTemperature> GetTemperatureObservable();

        Task<CpuAverageLoad> GetLastAverageLoadAsync();

        Task<IEnumerable<CpuAverageLoad>> GetAverageLoadsAsync();

        Task<CpuRealTimeLoad> GetLastRealTimeLoadAsync();

        Task<IEnumerable<CpuRealTimeLoad>> GetRealTimeLoadsAsync();

        Task SaveAsync();

        Task SaveTemperatureAsync();

        Task SaveAverageLoadAsync();

        Task SaveRealTimeLoadAsync();
    }
}
