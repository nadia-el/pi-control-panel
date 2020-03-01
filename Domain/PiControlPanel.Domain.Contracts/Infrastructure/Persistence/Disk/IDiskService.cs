namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk
{
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using System.Threading.Tasks;

    public interface IDiskService : IBaseService<Disk>
    {
        Task<Disk> GetAsync(string fileSystem);
    }
}
