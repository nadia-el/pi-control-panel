namespace PiControlPanel.Api.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Server.Transports.Subscriptions.Abstractions;
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Api.GraphQL.Types.Output.Cpu;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models.Hardware.Cpu;

    public class ControlPanelSubscription : ObjectGraphType
    {
        public ControlPanelSubscription(ICpuService cpuService, ILogger logger)
        {
            FieldSubscribe<CpuTemperatureType>(
                "Cpu",
                resolve: context =>
                {
                    return context.Source as Cpu;
                },
                subscribe: context =>
                {
                    logger.Info("Cpu subscription");
                    MessageHandlingContext messageHandlingContext = context.UserContext.As<MessageHandlingContext>();
                    GraphQLUserContext graphQLUserContext = messageHandlingContext.Get<GraphQLUserContext>("GraphQLUserContext");
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return cpuService.GetTemperatureObservable(businessContext);
                });
        }
    }
}
