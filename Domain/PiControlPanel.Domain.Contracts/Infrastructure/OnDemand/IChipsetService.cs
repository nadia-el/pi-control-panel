namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;

    public interface IChipsetService
    {
        Task<Chipset> GetAsync();
    }
}
