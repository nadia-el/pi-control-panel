namespace PiControlPanel.API.GraphQL.Types
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware;

    public class CpuType : ObjectGraphType<Cpu>
    {
        public CpuType()
        {
            Field(x => x.Temperature);
        }
    }
}
