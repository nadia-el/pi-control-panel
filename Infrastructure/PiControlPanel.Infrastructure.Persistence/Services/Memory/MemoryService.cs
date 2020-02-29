namespace PiControlPanel.Infrastructure.Persistence.Services.Memory
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Memory;
    using PiControlPanel.Domain.Models.Hardware.Memory;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class MemoryService : IMemoryService
    {
        private readonly IRepositoryBase<Entities.Memory.Memory> memoryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public MemoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.memoryRepository = unitOfWork.MemoryRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Memory> GetAsync()
        {
            var entity = await memoryRepository.GetAsync();
            return mapper.Map<Memory>(entity);
        }

        public async Task AddAsync(Memory memory)
        {
            var entity = mapper.Map<Entities.Memory.Memory>(memory);
            memoryRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Memory memory)
        {
            var entity = mapper.Map<Entities.Memory.Memory>(memory);
            memoryRepository.Update(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Memory memory)
        {
            var entity = mapper.Map<Entities.Memory.Memory>(memory);
            memoryRepository.Remove(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
