namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class ChipsetService : IChipsetService
    {
        private readonly IRepositoryBase<Entities.Chipset> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public ChipsetService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.repository = unitOfWork.ChipsetRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Chipset> GetAsync()
        {
            var entity = await repository.GetAsync();
            return mapper.Map<Chipset>(entity);
        }

        public async Task<Chipset> GetAsync(string serial)
        {
            var entity = await repository.GetAsync(c => c.Serial == serial);
            return mapper.Map<Chipset>(entity);
        }

        public async Task AddAsync(Chipset chipset)
        {
            var entity = mapper.Map<Entities.Chipset>(chipset);
            repository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Chipset chipset)
        {
            var entity = mapper.Map<Entities.Chipset>(chipset);
            repository.Update(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Chipset chipset)
        {
            var entity = mapper.Map<Entities.Chipset>(chipset);
            repository.Remove(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
