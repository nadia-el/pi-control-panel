namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu
{
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICpuAverageLoadService
    {
        Task<CpuAverageLoad> GetLastAsync();

        Task<IEnumerable<CpuAverageLoad>> GetAllAsync();

        Task AddAsync(CpuAverageLoad cpuAverageLoad);
    }
}
