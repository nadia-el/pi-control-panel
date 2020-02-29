namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models.Hardware;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class GpuService : IGpuService
    {
        private readonly IRepositoryBase<Entities.Gpu> gpuRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public GpuService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.gpuRepository = unitOfWork.GpuRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Gpu> GetAsync()
        {
            var entity = await gpuRepository.GetAsync();
            return mapper.Map<Gpu>(entity);
        }
        
        public async Task AddAsync(Gpu gpu)
        {
            var entity = mapper.Map<Entities.Gpu>(gpu);
            gpuRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Gpu gpu)
        {
            var entity = mapper.Map<Entities.Gpu>(gpu);
            gpuRepository.Update(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Gpu gpu)
        {
            var entity = mapper.Map<Entities.Gpu>(gpu);
            gpuRepository.Remove(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
