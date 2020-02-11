namespace PiControlPanel.Api.GraphQL.Types
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware;

    public class CpuTemperatureType : ObjectGraphType<Cpu>
    {
        public CpuTemperatureType()
        {
            Field(x => x.Temperature);
        }
    }
}
