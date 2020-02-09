namespace PiControlPanel.API.GraphQL.Schemas
{
    using global::GraphQL.Authorization;
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.API.GraphQL.Extensions;
    using PiControlPanel.API.GraphQL.Types;
    using PiControlPanel.API.GraphQL.Types.Input;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Models;

    public class ControlPanelQuery : ObjectGraphType
    {
        public ControlPanelQuery(ISecurityService securityService,
            IControlPanelService controlPanelService, ILogger logger)
        {
            FieldAsync<StringGraphType>(
                "Login",
                arguments: new QueryArguments(
                    new QueryArgument<UserAccountInputType> { Name = "UserAccount" }
                ),
                resolve: async context =>
                {
                    logger.Info("Login request");
                    var userAccount = context.GetArgument<UserAccount>("userAccount");

                    return await securityService.GenerateJsonWebTokenAsync(userAccount);
                });

            FieldAsync<HardwareType>(
                "Hardware",
                resolve: async context =>
                {
                    logger.Info("Hardware request");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    return await controlPanelService.GetHardwareAsync(businessContext);
                })
                .AuthorizeWith(AuthorizationPolicyName.AuthenticatedPolicy);
        }
    }
}
