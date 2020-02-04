namespace PiControlPanel.API.GraphQL.Types
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models;

    public class HardwareType : ObjectGraphType<Hardware>
    {
        public HardwareType()
        {
            Field(x => x.Cpu, type: typeof(CpuType));
        }
    }
}
