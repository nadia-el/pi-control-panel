namespace PiControlPanel.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using PiControlPanel.Domain.Contracts.Application;
    using PiControlPanel.Domain.Contracts.Constants;
    using PiControlPanel.Domain.Models;

    public class SecurityService : ISecurityService
    {
        public ClaimsIdentity CreateClaimsIdentity(UserAccount userAccount)
        {
            var identity = new ClaimsIdentity();

            if (userAccount != null)
            {
                identity.AddClaim(new Claim(CustomClaimTypes.UserId, userAccount.ID.ToString()));
                identity.AddClaim(new Claim(CustomClaimTypes.Username, userAccount.Username));
                identity.AddClaim(new Claim(CustomClaimTypes.IsAnonymous, false.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Individual));
            }
            else
            {
                identity.AddClaim(new Claim(CustomClaimTypes.IsAnonymous, true.ToString()));
            }

            return identity;
        }

        public string GenerateJSONWebToken(IDictionary<string, string> configuration, UserAccount userAccount)
        {
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
