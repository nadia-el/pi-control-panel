namespace PiControlPanel.Infrastructure.Persistence.Services.Cpu
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class CpuAverageLoadService : ICpuAverageLoadService
    {
        private readonly IRepositoryBase<Entities.Cpu.CpuAverageLoad> cpuAverageLoadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CpuAverageLoadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.cpuAverageLoadRepository = unitOfWork.CpuAverageLoadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CpuAverageLoad> GetLastAsync()
        {
            var entity = await cpuAverageLoadRepository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<CpuAverageLoad>(entity);
        }

        public async Task<IEnumerable<CpuAverageLoad>> GetAllAsync()
        {
            var entities = await cpuAverageLoadRepository.GetAll()
                .OrderByDescending(t => t.DateTime).ToListAsync();
            return mapper.Map<List<CpuAverageLoad>>(entities);
        }

        public async Task AddAsync(CpuAverageLoad cpuAverageLoad)
        {
            var entity = mapper.Map<Entities.Cpu.CpuAverageLoad>(cpuAverageLoad);
            cpuAverageLoadRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
