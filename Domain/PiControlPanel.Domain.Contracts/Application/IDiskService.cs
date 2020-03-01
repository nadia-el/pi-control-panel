namespace PiControlPanel.Domain.Contracts.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Disk;

    public interface IDiskService : IBaseService<Disk>
    {
        Task<DiskStatus> GetLastStatusAsync();

        Task<IEnumerable<DiskStatus>> GetStatusesAsync();

        Task SaveStatusAsync();

    }
}
