namespace PiControlPanel.Api.GraphQL.Extensions
{
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Models;
    using System.Linq;

    public static class GraphQLUserContextExtensions
    {
        public static BusinessContext GetBusinessContext(this GraphQLUserContext graphQLUserContext)
        {
            var isAnonymousClaim = graphQLUserContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.IsAnonymous);
            var usernameClaim = graphQLUserContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Username);

            var businessContext = new BusinessContext();

            if (isAnonymousClaim != null && bool.TryParse(isAnonymousClaim.Value, out bool isAnonymous))
                businessContext.IsAnonymous = isAnonymous;

            if (usernameClaim != null)
                businessContext.Username = usernameClaim.Value ?? string.Empty;

            return businessContext;
        }
    }
}
