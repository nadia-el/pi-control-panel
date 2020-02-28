namespace PiControlPanel.Infrastructure.Persistence.Services.Cpu
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Cpu;
    using PiControlPanel.Domain.Models.Hardware.Cpu;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class CpuService : ICpuService
    {
        private readonly IRepositoryBase<Entities.Cpu.Cpu> cpuRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public CpuService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.cpuRepository = unitOfWork.CpuRepository;
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
    }
}
