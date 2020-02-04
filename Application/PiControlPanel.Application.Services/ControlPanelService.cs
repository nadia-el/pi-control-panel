namespace PiControlPanel.Application.Services
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;
    using Infra = PiControlPanel.Domain.Contracts.Infrastructure;

    public class ControlPanelService : IControlPanelService
    {
        private readonly Infra.IControlPanelService infraService;

        public ControlPanelService(Infra.IControlPanelService infraService)
        {
            this.infraService = infraService;
        }

        public Task<Hardware> GetHardwareAsync(BusinessContext context)
        {
            return infraService.GetHardwareAsync(context);
        }
    }
}
