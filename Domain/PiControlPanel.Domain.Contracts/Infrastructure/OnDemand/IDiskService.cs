namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Disk;

    public interface IDiskService
    {
        Task<Disk> GetAsync();

        Task<DiskStatus> GetStatusAsync();
    }
}
