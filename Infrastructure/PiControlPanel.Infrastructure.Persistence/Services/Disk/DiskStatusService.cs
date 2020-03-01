namespace PiControlPanel.Infrastructure.Persistence.Services.Disk
{
    using AutoMapper;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk;
    using PiControlPanel.Domain.Models.Hardware.Disk;
    using PiControlPanel.Infrastructure.Persistence.Contracts.Repositories;
    
    public class DiskStatusService :
        BaseTimedService<DiskStatus, Entities.Disk.DiskStatus>,
        IDiskStatusService
    {
        public DiskStatusService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
            : base(unitOfWork, mapper, logger)
        {
            this.repository = unitOfWork.DiskStatusRepository;
        }
    }
}
