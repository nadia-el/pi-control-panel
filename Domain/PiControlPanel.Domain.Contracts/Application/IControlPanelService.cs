namespace PiControlPanel.Domain.Contracts.Application
{
    using System;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;

    public interface IControlPanelService
    {
        Task<Hardware> GetHardwareAsync(BusinessContext context);

        IObservable<Hardware> GetHardwareObservable(BusinessContext context);
    }
}
