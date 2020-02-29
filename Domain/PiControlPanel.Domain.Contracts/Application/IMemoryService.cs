namespace PiControlPanel.Domain.Contracts.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Memory;

    public interface IMemoryService
    {
        Task<Memory> GetAsync();

        Task<MemoryStatus> GetLastStatusAsync();

        Task<IEnumerable<MemoryStatus>> GetStatusesAsync();

        Task SaveAsync();

        Task SaveStatusAsync();
    }
}
