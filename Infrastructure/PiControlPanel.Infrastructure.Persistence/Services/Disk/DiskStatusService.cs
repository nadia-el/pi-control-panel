namespace PiControlPanel.Infrastructure.Persistence.Services.Disk
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk;
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class DiskStatusService : IDiskStatusService
    {
        private readonly IRepositoryBase<Entities.Disk.DiskStatus> diskStatusRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public DiskStatusService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            this.diskStatusRepository = unitOfWork.DiskStatusRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<DiskStatus> GetLastAsync()
        {
            var entity = await diskStatusRepository.GetAll()
                .OrderByDescending(t => t.DateTime).FirstOrDefaultAsync();
            return mapper.Map<DiskStatus>(entity);
        }

        public async Task<IEnumerable<DiskStatus>> GetAllAsync()
        {
            var entities = await diskStatusRepository.GetAll()
                .OrderByDescending(t => t.DateTime).ToListAsync();
            return mapper.Map<List<DiskStatus>>(entities);
        }

        public async Task AddAsync(DiskStatus diskStatus)
        {
            var entity = mapper.Map<Entities.Disk.DiskStatus>(diskStatus);
            diskStatusRepository.Create(entity);
            await this.unitOfWork.CommitAsync();
        }
    }
}
