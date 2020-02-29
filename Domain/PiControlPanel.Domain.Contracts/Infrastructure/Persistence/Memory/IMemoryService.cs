namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Memory
{
    using PiControlPanel.Domain.Models.Hardware.Memory;
    using System.Threading.Tasks;

    public interface IMemoryService
    {
        Task<Memory> GetAsync();

        Task AddAsync(Memory disk);

        Task UpdateAsync(Memory disk);

        Task RemoveAsync(Memory disk);
    }
}
