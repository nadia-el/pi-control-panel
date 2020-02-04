namespace PiControlPanel.API.GraphQL.Schemas
{
    using global::GraphQL.Authorization;
    using global::GraphQL.Types;
    using PiControlPanel.API.GraphQL.Extensions;
    using PiControlPanel.API.GraphQL.Types;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Contracts.Constants;

    public class ControlPanelQuery : ObjectGraphType
    {
        public ControlPanelQuery(IControlPanelService controlPanelService)
        {
            //this.AuthorizeWith(AuthorizationPolicyName.Authenticated);
            //this.AuthorizeWith(AuthorizationPolicyName.Individual);

            FieldAsync<HardwareType>(
                "Hardware",
                resolve: async context =>
                {
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await controlPanelService.GetHardwareAsync(businessContext);
                });
        }
    }
}
