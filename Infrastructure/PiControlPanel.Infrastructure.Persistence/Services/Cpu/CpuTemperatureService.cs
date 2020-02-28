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
    
    public class CpuTemperatureService : ICpuTemperatureService
    {
        private readonly IRepositoryBase<Entities.Cpu.CpuTemperature> cpuTemperatureRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CpuTemperatureService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.cpuTemperatureRepository = unitOfWork.CpuTemperatureRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CpuTemperature> GetLastAsync()
        {
            var entity = await cpuTemperatureRepository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<CpuTemperature>(entity);
        }

        public async Task<IEnumerable<CpuTemperature>> GetAllAsync()
        {
            var entities = await cpuTemperatureRepository.GetAll()
                .OrderByDescending(t => t.DateTime).ToListAsync();
            return mapper.Map<List<CpuTemperature>>(entities);
        }

        public async Task AddAsync(CpuTemperature cpuTemperature)
        {
            var entity = mapper.Map<Entities.Cpu.CpuTemperature>(cpuTemperature);
            cpuTemperatureRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
