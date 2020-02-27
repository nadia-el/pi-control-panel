namespace PiControlPanel.Api.GraphQL.Types.Output.Cpu
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public class CpuAverageLoadType : ObjectGraphType<CpuAverageLoad>
    {
        public CpuAverageLoadType()
        {
            Field(x => x.LastMinute);
            Field(x => x.Last5Minutes);
            Field(x => x.Last15Minutes);
            Field<DateTimeGraphType>("dateTime");
        }
    }
}
