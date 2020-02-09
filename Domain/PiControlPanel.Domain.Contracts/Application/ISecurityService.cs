namespace PiControlPanel.Domain.Contracts.Application
{
    using PiControlPanel.Domain.Models;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface ISecurityService
    {
        ClaimsIdentity CreateClaimsIdentity(UserAccount userAccount);

        Task<string> GenerateJsonWebTokenAsync(UserAccount userAccount);
    }
}
