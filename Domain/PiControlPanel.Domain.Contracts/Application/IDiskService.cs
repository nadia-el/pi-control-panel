namespace PiControlPanel.Domain.Contracts.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Disk;

    public interface IDiskService
    {
        Task<Disk> GetAsync();

        Task<DiskStatus> GetLastStatusAsync();

        Task<IEnumerable<DiskStatus>> GetStatusesAsync();

        Task SaveAsync();

        Task SaveStatusAsync();

    }
}
