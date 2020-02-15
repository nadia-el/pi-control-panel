namespace PiControlPanel.Domain.Contracts.Application
{
    using System.Threading.Tasks;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Domain.Models.Hardware;

    public interface IMemoryService
    {
        Task<Memory> GetAsync(BusinessContext context);
    }
}
