namespace PiControlPanel.Api.GraphQL.Types.Output.Disk
{
    using global::GraphQL.Types;
    using PiControlPanel.Domain.Models.Hardware.Disk;

    public class DiskStatusType : ObjectGraphType<DiskStatus>
    {
        public DiskStatusType()
        {
            Field(x => x.Used);
            Field(x => x.Available);
            Field<DateTimeGraphType>("dateTime");
        }
    }
}
