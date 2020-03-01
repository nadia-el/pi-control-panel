namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware.Memory;

    public interface IMemoryService : IBaseService<Memory>
    {
        Task<MemoryStatus> GetStatusAsync();
    }
}
