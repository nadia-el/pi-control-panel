namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk
{
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDiskStatusService
    {
        Task<DiskStatus> GetLastAsync();

        Task<IEnumerable<DiskStatus>> GetAllAsync();

        Task AddAsync(DiskStatus diskStatus);
    }
}
