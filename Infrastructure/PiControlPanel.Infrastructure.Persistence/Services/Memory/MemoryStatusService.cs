namespace PiControlPanel.Infrastructure.Persistence.Services.Memory
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Memory;
    using PiControlPanel.Domain.Models.Hardware.Memory;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class MemoryStatusService : IMemoryStatusService
    {
        private readonly IRepositoryBase<Entities.Memory.MemoryStatus> memoryStatusRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public MemoryStatusService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.memoryStatusRepository = unitOfWork.MemoryStatusRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<MemoryStatus> GetLastAsync()
        {
            var entity = await memoryStatusRepository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<MemoryStatus>(entity);
        }

        public async Task<IEnumerable<MemoryStatus>> GetAllAsync()
        {
            var entities = await memoryStatusRepository.GetAll()
                .OrderByDescending(t => t.DateTime).ToListAsync();
            return mapper.Map<List<MemoryStatus>>(entities);
        }

        public async Task AddAsync(MemoryStatus memoryStatus)
        {
            var entity = mapper.Map<Entities.Memory.MemoryStatus>(memoryStatus);
            memoryStatusRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
