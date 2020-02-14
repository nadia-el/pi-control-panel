namespace PiControlPanel.Api.GraphQL.Types.Output.Cpu
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public class CpuTemperatureType : ObjectGraphType<Cpu>
    {
        public CpuTemperatureType()
        {
            Field(x => x.Temperature);
        }
    }
}
