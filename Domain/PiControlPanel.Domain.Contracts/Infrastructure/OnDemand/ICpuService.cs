namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;

    public interface ICpuService
    {
        Task<Cpu> GetAsync(BusinessContext context);

        Task<double> GetTemperatureAsync(BusinessContext context);

        void PublishStatus();

        IObservable<Cpu> GetObservable(BusinessContext context);
    }
}
