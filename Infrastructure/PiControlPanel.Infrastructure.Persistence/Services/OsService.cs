namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class OsService : IOsService
    {
        private readonly IRepositoryBase<Entities.Os> osRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public OsService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.osRepository = unitOfWork.OsRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Os> GetAsync()
        {
            var entity = await osRepository.GetAsync();
            return mapper.Map<Os>(entity);
        }
        
        public async Task AddAsync(Os os)
        {
            var entity = mapper.Map<Entities.Os>(os);
            osRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Os os)
        {
            var entity = mapper.Map<Entities.Os>(os);
            osRepository.Update(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Os os)
        {
            var entity = mapper.Map<Entities.Os>(os);
            osRepository.Remove(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
