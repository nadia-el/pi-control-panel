namespace PiControlPanel.Application.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using NLog;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Contracts.Infrastructure.Persistence;
    using PiControlPanel.Domain.Models;

    public class SecurityService : ISecurityService
    {
        private readonly IUserAccountService userAccountService;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public SecurityService(IUserAccountService userAccountService,
            IConfiguration configuration, ILogger logger)
        {
            this.userAccountService = userAccountService;
            this.configuration = configuration;
            this.logger = logger;
        }

        public ClaimsIdentity CreateClaimsIdentity(UserAccount userAccount)
        {
            logger.Info("Application layer -> CreateClaimsIdentity");
            var identity = new ClaimsIdentity();

            if (userAccount != null)
            {
                logger.Info($"Creating claims for user {userAccount.Username}");
                identity.AddClaim(new Claim(CustomClaimTypes.Username, userAccount.Username));
                identity.AddClaim(new Claim(CustomClaimTypes.IsAnonymous, false.ToString()));
                identity.AddClaim(new Claim(CustomClaimTypes.IsAuthenticated, true.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Individual));
            }
            else
            {
                identity.AddClaim(new Claim(CustomClaimTypes.IsAnonymous, true.ToString()));
            }

            return identity;
        }

        public async Task<string> GenerateJsonWebTokenAsync(UserAccount userAccount)
        {
            if (userAccount == null ||
                string.IsNullOrWhiteSpace(userAccount.Username) ||
                string.IsNullOrWhiteSpace(userAccount.Password))
            {
                logger.Error("Missing user account information");
                return null; // TODO: throw business error?
            }

            var isUserAccountValid = await userAccountService.ValidateAsync(userAccount);
            if (!isUserAccountValid)
            {
                logger.Error("Invalid user account");
                return null; // TODO: throw business error?
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                CreateClaimsIdentity(userAccount).Claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
