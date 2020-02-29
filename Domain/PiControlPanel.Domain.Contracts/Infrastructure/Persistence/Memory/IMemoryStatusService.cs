namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Memory
{
    using PiControlPanel.Domain.Models.Hardware.Memory;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMemoryStatusService
    {
        Task<MemoryStatus> GetLastAsync();

        Task<IEnumerable<MemoryStatus>> GetAllAsync();

        Task AddAsync(MemoryStatus memoryStatus);
    }
}
