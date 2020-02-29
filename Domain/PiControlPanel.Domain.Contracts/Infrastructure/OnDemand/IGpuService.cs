namespace PiControlPanel.Domain.Contracts.Infrastructure.OnDemand
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware;

    public interface IGpuService
    {
        Task<Gpu> GetAsync();
    }
}
