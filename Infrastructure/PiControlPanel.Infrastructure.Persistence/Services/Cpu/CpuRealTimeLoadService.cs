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
    
    public class CpuRealTimeLoadService : ICpuRealTimeLoadService
    {
        private readonly IRepositoryBase<Entities.Cpu.CpuRealTimeLoad> cpuRealTimeLoadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CpuRealTimeLoadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.cpuRealTimeLoadRepository = unitOfWork.CpuRealTimeLoadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CpuRealTimeLoad> GetLastAsync()
        {
            var entity = await cpuRealTimeLoadRepository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<CpuRealTimeLoad>(entity);
        }

        public async Task<IEnumerable<CpuRealTimeLoad>> GetAllAsync()
        {
            var entities = await cpuRealTimeLoadRepository.GetAll()
                .OrderByDescending(t => t.DateTime).ToListAsync();
            return mapper.Map<List<CpuRealTimeLoad>>(entities);
        }

        public async Task AddAsync(CpuRealTimeLoad cpuRealTimeLoad)
        {
            var entity = mapper.Map<Entities.Cpu.CpuRealTimeLoad>(cpuRealTimeLoad);
            cpuRealTimeLoadRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
