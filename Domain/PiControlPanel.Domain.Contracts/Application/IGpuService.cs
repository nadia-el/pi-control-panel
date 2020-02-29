namespace PiControlPanel.Domain.Contracts.Application
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models.Hardware;

    public interface IGpuService
    {
        Task<Gpu> GetAsync();

        Task SaveAsync();
    }
}
