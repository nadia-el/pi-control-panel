namespace PiControlPanel.API.GraphQL.Extensions
{
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Models;
    using System;
    using System.Linq;

    public static class GraphQLUserContextExtensions
    {
        public static BusinessContext GetBusinessContext(this GraphQLUserContext graphQLUserContext)
        {
            var isAnonymousClaim = graphQLUserContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.IsAnonymous);
            var userIdClaim = graphQLUserContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.UserId);
            var usernameClaim = graphQLUserContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Username);

            var businessContext = new BusinessContext();

            if (isAnonymousClaim != null && bool.TryParse(isAnonymousClaim.Value, out bool isAnonymous))
                businessContext.IsAnonymous = isAnonymous;

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                businessContext.UserId = userId;

            if (usernameClaim != null)
                businessContext.Username = usernameClaim.Value ?? string.Empty;

            return businessContext;
        }
    }
}
