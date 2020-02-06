namespace PiControlPanel.Domain.Contracts.Infrastructure.Persistence
{
    using PiControlPanel.Domain.Models;
    using System;
    using System.Threading.Tasks;

    public interface IUserAccountService
    {
        Task<bool> ValidateAsync(UserAccount userAccount);
    }
}
