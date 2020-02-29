namespace PiControlPanel.Api.GraphQL.Types.Output
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware.Memory;

    public class MemoryStatusType : ObjectGraphType<MemoryStatus>
    {
        public MemoryStatusType()
        {
            Field(x => x.Used);
            Field(x => x.Available);
            Field<DateTimeGraphType>("dateTime");
        }
    }
}
