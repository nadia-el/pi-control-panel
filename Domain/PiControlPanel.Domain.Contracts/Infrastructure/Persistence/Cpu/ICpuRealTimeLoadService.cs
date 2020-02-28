namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu
{
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICpuRealTimeLoadService
    {
        Task<CpuRealTimeLoad> GetLastAsync();

        Task<IEnumerable<CpuRealTimeLoad>> GetAllAsync();

        Task AddAsync(CpuRealTimeLoad cpuRealTimeLoad);
    }
}
