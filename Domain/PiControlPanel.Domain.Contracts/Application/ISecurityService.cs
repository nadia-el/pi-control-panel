namespace PiControlPanel.Domain.Contracts.Application
{
    using PiControlPanel.Domain.Models;
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface ISecurityService
    {
        ClaimsIdentity CreateClaimsIdentity(UserAccount userAccount);

        string GenerateJSONWebToken(IDictionary<string, string> configuration, UserAccount userAccount);
    }
}
