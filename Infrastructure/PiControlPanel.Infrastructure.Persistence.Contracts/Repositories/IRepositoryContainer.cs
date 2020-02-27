namespace PiControlPanel.Infrastructure.Persistence.Contracts.Repositories
{
    public interface IRepositoryContainer
    {
        IRepositoryBase<Entities.Chipset> ChipsetRepository { get; }

        IRepositoryBase<Entities.Cpu.Cpu> CpuRepository { get; }

        IRepositoryBase<Entities.Cpu.CpuTemperature> CpuTemperatureRepository { get; }
    }
}
