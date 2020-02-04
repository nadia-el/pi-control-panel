namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using PiControlPanel.Domain.Contracts.Infrastructure;
    using PiControlPanel.Domain.Models;
    using System.Threading.Tasks;

    public class UserAccountService : IUserAccountService
    {
        public Task<bool> ValidateAsync(UserAccount userAccount)
        {
            return Task.FromResult(true);
        }
    }
}
