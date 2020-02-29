namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk
{
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using System.Threading.Tasks;

    public interface IDiskService
    {
        Task<Disk> GetAsync();

        Task<Disk> GetAsync(string fileSystem);

        Task AddAsync(Disk disk);

        Task UpdateAsync(Disk disk);

        Task RemoveAsync(Disk disk);
    }
}
