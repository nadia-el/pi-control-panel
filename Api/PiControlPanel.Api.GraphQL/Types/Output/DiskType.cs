namespace PiControlPanel.Api.GraphQL.Types.Output
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware;

    public class DiskType : ObjectGraphType<Disk>
    {
        public DiskType()
        {
            Field(x => x.FileSystem);
            Field(x => x.Type);
            Field(x => x.Total);
            Field(x => x.Used);
            Field(x => x.Available);
        }
    }
}
