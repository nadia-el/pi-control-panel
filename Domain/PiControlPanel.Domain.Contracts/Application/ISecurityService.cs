namespace PiControlPanel.Domain.Contracts.Application
{
    using PiControlPanel.Domain.Models;
    using System.Threading.Tasks;

    public interface ISecurityService
    {
        Task<string> GenerateJsonWebTokenAsync(UserAccount userAccount);
    }
}
