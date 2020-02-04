namespace PiControlPanel.Domain.Contracts.Infrastructure
{
    using PiControlPanel.Domain.Models;
    using System;
    using System.Threading.Tasks;

    public interface IUserAccountService
    {
        Task<bool> ValidateAsync(UserAccount userAccount);
    }
}
