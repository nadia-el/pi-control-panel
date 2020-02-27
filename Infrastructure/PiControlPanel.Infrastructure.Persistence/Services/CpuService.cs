namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class CpuService : ICpuService
    {
        private readonly IRepositoryBase<Entities.Cpu.Cpu> cpuRepository;
        private readonly IRepositoryBase<Entities.Cpu.CpuTemperature> cpuTemperatureRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CpuService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.cpuRepository = unitOfWork.CpuRepository;
            this.cpuTemperatureRepository = unitOfWork.CpuTemperatureRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Cpu> GetAsync()
        {
            var entity = await cpuRepository.GetAsync();
            return mapper.Map<Cpu>(entity);
        }

        public async Task<Cpu> GetAsync(string model)
        {
            var entity = await cpuRepository.GetAsync(c => c.Model == model);
            return mapper.Map<Cpu>(entity);
        }

        public async Task AddAsync(Cpu cpu)
        {
            var entity = mapper.Map<Entities.Cpu.Cpu>(cpu);
            cpuRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Cpu cpu)
        {
            var entity = mapper.Map<Entities.Cpu.Cpu>(cpu);
            cpuRepository.Update(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Cpu cpu)
        {
            var entity = mapper.Map<Entities.Cpu.Cpu>(cpu);
            cpuRepository.Remove(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task<CpuTemperature> GetLastTemperatureAsync()
        {
            var entity = await cpuTemperatureRepository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<CpuTemperature>(entity);
        }

        public async Task<IEnumerable<CpuTemperature>> GetTemperaturesAsync()
        {
            var entities = await cpuTemperatureRepository.GetAll()
                .OrderByDescending(t => t.DateTime).ToListAsync();
            return mapper.Map<List<CpuTemperature>>(entities);
        }

        public async Task AddTemperatureAsync(CpuTemperature cpuTemperature)
        {
            var entity = mapper.Map<Entities.Cpu.CpuTemperature>(cpuTemperature);
            cpuTemperatureRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
