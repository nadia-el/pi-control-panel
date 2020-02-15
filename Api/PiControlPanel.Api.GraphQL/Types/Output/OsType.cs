namespace PiControlPanel.Api.GraphQL.Types.Output
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware;

    public class OsType : ObjectGraphType<Os>
    {
        public OsType()
        {
            Field(x => x.Name);
            Field(x => x.Kernel);
            Field(x => x.Hostname);
        }
    }
}
