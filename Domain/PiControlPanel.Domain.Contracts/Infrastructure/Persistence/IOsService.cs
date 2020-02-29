namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence
{
    using PiControlPanel.Domain.Models.Hardware;
    using System.Threading.Tasks;

    public interface IOsService
    {
        Task<Os> GetAsync();

        Task AddAsync(Os os);

        Task UpdateAsync(Os os);

        Task RemoveAsync(Os os);
    }
}
