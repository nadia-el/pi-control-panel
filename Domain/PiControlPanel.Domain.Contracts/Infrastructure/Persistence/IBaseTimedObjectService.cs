namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence
{
    using PiControlPanel.Domain.Models.Hardware;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseTimedObjectService<T> where T : BaseTimedObject
    {
        Task<T> GetLastAsync();

        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T model);
    }
}
