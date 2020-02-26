namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence
{
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;

    public interface IChipsetService
    {
        Task<Chipset> GetAsync();

        Task<Chipset> GetAsync(string serial);

        Task AddAsync(Chipset chipset);

        Task UpdateAsync(Chipset chipset);

        Task RemoveAsync(Chipset chipset);
    }
}
