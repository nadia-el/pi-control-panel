namespace PiControlPanel.Infrastructure.Persistence.Services.Disk
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk;
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class DiskService : IDiskService
    {
        private readonly IRepositoryBase<Entities.Disk.Disk> diskRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DiskService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.diskRepository = unitOfWork.DiskRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Disk> GetAsync()
        {
            var entity = await diskRepository.GetAsync();
            return mapper.Map<Disk>(entity);
        }

        public async Task<Disk> GetAsync(string fileSystem)
        {
            var entity = await diskRepository.GetAsync(c => c.FileSystem == fileSystem);
            return mapper.Map<Disk>(entity);
        }

        public async Task AddAsync(Disk disk)
        {
            var entity = mapper.Map<Entities.Disk.Disk>(disk);
            diskRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Disk disk)
        {
            var entity = mapper.Map<Entities.Disk.Disk>(disk);
            diskRepository.Update(entity);
            await this.unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(Disk disk)
        {
            var entity = mapper.Map<Entities.Disk.Disk>(disk);
            diskRepository.Remove(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
