namespace PiControlPanel.Api.GraphQL.Types
{
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Domain.Contracts.Application;

    public class HardwareType : ObjectGraphType
    {
        public HardwareType(ICpuService cpuService, ILogger logger)
        {
            Field<CpuType>()
                .Name("Cpu")
                .ResolveAsync(async context =>
                {
                    logger.Info("Cpu field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await cpuService.GetAsync(businessContext);
                });
        }
    }
}
