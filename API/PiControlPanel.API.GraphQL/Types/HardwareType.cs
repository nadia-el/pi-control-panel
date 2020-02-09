namespace PiControlPanel.API.GraphQL.Types
{
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.API.GraphQL.Extensions;
    using PiControlPanel.Domain.Contracts.Application;

    public class HardwareType : ObjectGraphType
    {
        public HardwareType(IControlPanelService controlPanelService, ILogger logger)
        {
            Field<CpuType>()
                .Name("Cpu")
                .ResolveAsync(async context =>
                {
                    logger.Info("Cpu type");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await controlPanelService.GetCpuAsync(businessContext);
                });
        }
    }
}
