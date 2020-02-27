namespace PiControlPanel.Api.GraphQL.Types.Output.Cpu
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public class CpuRealTimeLoadType : ObjectGraphType<CpuRealTimeLoad>
    {
        public CpuRealTimeLoadType()
        {
            Field(x => x.Kernel);
            Field(x => x.User);
            Field(x => x.Total);
            Field<DateTimeGraphType>("dateTime");
        }
    }
}
