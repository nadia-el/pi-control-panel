namespace PiControlPanel.Api.GraphQL.Types.Output
{
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Api.GraphQL.Types.Output.Cpu;
    using PiControlPanel.Domain.Contracts.Application;

    public class HardwareType : ObjectGraphType
    {
        public HardwareType(ICpuService cpuService, IChipsetService chipsetService,
            ILogger logger)
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

            Field<ChipsetType>()
                .Name("Chipset")
                .ResolveAsync(async context =>
                {
                    logger.Info("Chipset field");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await chipsetService.GetAsync(businessContext);
                });
        }
    }
}
