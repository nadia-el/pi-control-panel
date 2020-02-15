namespace PiControlPanel.Api.GraphQL.Types.Output
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware;

    public class MemoryType : ObjectGraphType<Memory>
    {
        public MemoryType()
        {
            Field(x => x.Total);
            Field(x => x.Used);
            Field(x => x.Available);
        }
    }
}
