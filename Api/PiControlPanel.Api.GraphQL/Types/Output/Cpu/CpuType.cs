namespace PiControlPanel.Api.GraphQL.Types.Output.Cpu
{
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public class CpuType : ObjectGraphType<Cpu>
    {
        public CpuType(ICpuService cpuService, ILogger logger)
        {
            Field(x => x.Cores);
            Field(x => x.Model);
            Field<CpuLoadType>()
                .Name("Load")
                .Resolve(context =>
                {
                    logger.Info("Load field");

                    // Retuning empty object to make GraphQL resolve the CpuLoadType fields
                    // https://graphql-dotnet.github.io/docs/getting-started/query-organization/
                    return new { };
                });
            Field<FloatGraphType>()
                .Name("Temperature")
                .ResolveAsync(async context =>
                {
                    logger.Info("Temperature field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await cpuService.GetTemperatureAsync(businessContext);
                });
        }
    }
}
