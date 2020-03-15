namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    using PiControlPanel.Infrastructure.Persistence.Entities;

    public abstract class BaseTimedService<T, U> : IBaseTimedObjectService<T>
        where T : BaseTimedObject
        where U : BaseTimedEntity
    {
        protected IRepositoryBase<U> repository;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        protected readonly ILogger logger;

        public BaseTimedService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<T> GetLastAsync()
        {
            var entity = await repository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<T>(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await repository.GetAll()
                .OrderBy(t => t.DateTime).ToListAsync();
            return mapper.Map<List<T>>(entities);
        }

        public async Task AddAsync(T model)
        {
            var entity = mapper.Map<U>(model);
            repository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
