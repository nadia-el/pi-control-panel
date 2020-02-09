namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;

    public interface IControlPanelService
    {
        Task<Cpu> GetCpuAsync(BusinessContext context);

        IObservable<Cpu> GetCpuObservable(BusinessContext context);

        Task<bool> ShutdownAsync(BusinessContext context);
    }
}
