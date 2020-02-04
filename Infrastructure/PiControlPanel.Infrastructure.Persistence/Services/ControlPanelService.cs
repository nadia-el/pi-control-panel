namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using PiControlPanel.Domain.Contracts.Infrastructure;
    using PiControlPanel.Domain.Models;
    using System.Threading.Tasks;

    public class ControlPanelService : IControlPanelService
    {
        public Task<Hardware> GetHardwareAsync(BusinessContext context)
        {
            return Task.FromResult(new Hardware()
            {
                Cpu = new Cpu()
                {
                    Temperature = 35.4
                }
            });
        }
    }
}
