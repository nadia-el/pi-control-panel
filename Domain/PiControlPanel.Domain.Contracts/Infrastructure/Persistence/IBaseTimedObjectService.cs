namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence
{
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Domain.Models.Paging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseTimedObjectService<T> where T : BaseTimedObject
    {
        Task<T> GetLastAsync();

        Task<IEnumerable<T>> GetAllAsync();

        Task<PagingOutput<T>> GetPageAsync(PagingInput pagingInput);

        Task AddAsync(T model);
    }
}
