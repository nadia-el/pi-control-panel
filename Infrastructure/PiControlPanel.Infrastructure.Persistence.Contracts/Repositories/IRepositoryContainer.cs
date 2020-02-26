namespace PiControlPanel.Infrastructure.Persistence.Contracts.Repositories
{
    using PiControlPanel.Infrastructure.Persistence.Entities;

    public interface IRepositoryContainer
    {
        IRepositoryBase<Chipset> ChipsetRepository { get; }
    }
}
