namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence.Disk
{
    using PiControlPanel.Domain.Models.Hardware.Disk;

    public interface IDiskStatusService : IBaseTimedObjectService<DiskStatus>
    {
    }
}
