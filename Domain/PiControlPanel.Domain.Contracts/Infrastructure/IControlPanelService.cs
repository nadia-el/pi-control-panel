namespace PiControlPanel.Domain.Contracts.Infrastructure
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;

    public interface IControlPanelService
    {
        Task<Hardware> GetHardwareAsync(BusinessContext context);
    }
}
