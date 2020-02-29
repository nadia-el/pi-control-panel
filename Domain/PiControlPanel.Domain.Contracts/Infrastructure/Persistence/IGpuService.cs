namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence
{
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;

    public interface IGpuService
    {
        Task<Gpu> GetAsync();

        Task AddAsync(Gpu cpu);

        Task UpdateAsync(Gpu cpu);

        Task RemoveAsync(Gpu cpu);
    }
}
