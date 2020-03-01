namespace PiControlPanel.Infrastructure.Persistence.Services.Disk
{
    using System.Threading.Tasks;
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk;
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class DiskService : BaseService<Disk, Entities.Disk.Disk>, IDiskService
    {
        public DiskService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
            : base(unitOfWork, mapper, logger)
        {
            this.repository = unitOfWork.DiskRepository;
        }

        public async Task<Disk> GetAsync(string fileSystem)
        {
            var entity = await repository.GetAsync(c => c.FileSystem == fileSystem);
            return mapper.Map<Disk>(entity);
        }
    }
}
