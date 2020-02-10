namespace PiControlPanel.Api.GraphQL.Schemas
{
    using global::GraphQL.Authorization;
    using global::GraphQL.Types;
    using NLog;
    using PiControlPanel.Api.GraphQL.Extensions;
    using PiControlPanel.Api.GraphQL.Types;
    using PiControlPanel.Api.GraphQL.Types.Input;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Models;

    public class ControlPanelQuery : ObjectGraphType
    {
        public ControlPanelQuery(ISecurityService securityService, ILogger logger)
        {
            FieldAsync<StringGraphType>(
                "Login",
                arguments: new QueryArguments(
                    new QueryArgument<UserAccountInputType> { Name = "UserAccount" }
                ),
                resolve: async context =>
                {
                    logger.Info("Login query");
                    var userAccount = context.GetArgument<UserAccount>("userAccount");

                    return await securityService.GenerateJsonWebTokenAsync(userAccount);
                });

            Field<HardwareType>(
                "Hardware",
                resolve: context =>
                {
                    logger.Info("Hardware query");
                    GraphQLUserContext graphQLUserContext = context.UserContext as GraphQLUserContext;
                    var businessContext = graphQLUserContext.GetBusinessContext();

                    // Retuning empty object to make GraphQL resolve the HardwareType fields
                    // https://graphql-dotnet.github.io/docs/getting-started/query-organization/
                    return new { };
                })
                .AuthorizeWith(AuthorizationPolicyName.AuthenticatedPolicy);
        }
    }
}
