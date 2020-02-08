namespace PiControlPanel.API.GraphQL.Schemas
{
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.API.GraphQL.Extensions;
    using PiControlPanel.Domain.Contracts.Application;

    public class ControlPanelMutation : ObjectGraphType
    {
        public ControlPanelMutation(IControlPanelService controlPanelService, ILogger logger)
        {
            //this.AuthorizeWith(AuthorizationPolicyName.Authenticated);
            //this.AuthorizeWith(AuthorizationPolicyName.Individual);

            FieldAsync<BooleanGraphType>(
                "Shutdown",
                resolve: async context =>
                {
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    await controlPanelService.ShutdownAsync(businessContext);
                    return true;
                });
        }
    }
}
