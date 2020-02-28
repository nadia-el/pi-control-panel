namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu
{
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICpuTemperatureService
    {
        Task<CpuTemperature> GetLastAsync();

        Task<IEnumerable<CpuTemperature>> GetAllAsync();

        Task AddAsync(CpuTemperature cpuTemperature);
    }
}
