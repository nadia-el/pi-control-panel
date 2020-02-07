namespace PiControlPanel.API.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Server.Transports.Subscriptions.Abstractions;
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.API.GraphQL.Extensions;
    using PiControlPanel.API.GraphQL.Types;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Models;

    public class ControlPanelSubscription : ObjectGraphType
    {
        public ControlPanelSubscription(IControlPanelService controlPanelService, ILogger logger)
        {
            FieldSubscribe<HardwareType>(
                "Hardware",
                resolve: context =>
                {
                    return context.Source as Hardware;
                },
                subscribe: context =>
                {
                    //MessageHandlingContext messageHandlingContext = context.UserContext.As<MessageHandlingContext>();
                    //GraphQLUserContext graphQLUserContext = messageHandlingContext.Get<GraphQLUserContext>("GraphQLUserContext");
                    //var businessContext = graphQLUserContext.GetBusinessContext();
                    BusinessContext businessContext = null;

                    return controlPanelService.GetHardwareObservable(businessContext);
                });
        }
    }
}
