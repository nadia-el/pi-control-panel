namespace PiControlPanel.Infrastructure.Persistence.Services
{
    using System;
    using System.Threading.Tasks;
    using NLog;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models;
    using PiControlPanel.Infrastructure.Common;

    public class UserAccountService : IUserAccountService
    {
        private readonly ILogger logger;

        public UserAccountService(ILogger logger)
        {
            this.logger = logger;
        }

        public Task<bool> ValidateAsync(UserAccount userAccount)
        {
            logger.Info("Infra layer -> ValidateAsync");

            var catEtcShadowCommand = string.Format(
                Constants.CatEtcShadowCommand,
                userAccount.Username);
            var loginInfo = catEtcShadowCommand.Bash();
            logger.Debug($"Result of CatEtcShadowCommand: '{loginInfo}'");

            if (string.IsNullOrWhiteSpace(loginInfo))
            {
                logger.Error($"User {userAccount.Username} not found");
                return Task.FromResult(false);
            }

            var parsedLoginInfo = loginInfo.Split(':');
            if (!userAccount.Username.Equals(parsedLoginInfo[0]))
            {
                logger.Error($"Found username {parsedLoginInfo[0]} different from searched {userAccount.Username}");
                return Task.FromResult(false);
            }

            var passwordInfo = parsedLoginInfo[1].Split('$');
            var openSslPasswdCommand = string.Format(
                Constants.OpenSslPasswdCommand,
                passwordInfo[1],
                passwordInfo[2],
                userAccount.Password);
            var hashedPassword = openSslPasswdCommand.Bash();
            logger.Debug($"Result of OpenSslPasswdCommand: '{hashedPassword}'");
            //if (!parsedLoginInfo[1].Equals(hashedPassword))
            if (!string.Equals(parsedLoginInfo[1], hashedPassword, StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error($"Hashed password {hashedPassword} different from existing hashed password {parsedLoginInfo[1]}");
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> IsSuperUserAsync(UserAccount userAccount)
        {
            logger.Info("Infra layer -> IsSuperUserAsync");

            var groupsCommand = string.Format(
                Constants.GroupsCommand,
                userAccount.Username);
            var result = groupsCommand.Bash();
            logger.Debug($"Result of Groups from command: '{result}'");
            return Task.FromResult(result.Contains(" sudo "));
        }
    }
}
