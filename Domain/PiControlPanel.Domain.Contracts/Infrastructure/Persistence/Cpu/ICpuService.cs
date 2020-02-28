namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu
{
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using System.Threading.Tasks;

    public interface ICpuService
    {
        Task<Cpu> GetAsync();

        Task<Cpu> GetAsync(string model);

        Task AddAsync(Cpu cpu);

        Task UpdateAsync(Cpu cpu);

        Task RemoveAsync(Cpu cpu);
    }
}
