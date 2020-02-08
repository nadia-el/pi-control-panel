namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;

    public interface IControlPanelService
    {
        Task<Hardware> GetHardwareAsync(BusinessContext context);

        void PublishHardware();

        IObservable<Hardware> GetHardwareObservable(BusinessContext context);

        Task<bool> ShutdownAsync(BusinessContext context);
    }
}
