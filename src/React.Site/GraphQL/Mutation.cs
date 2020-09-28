using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotChocolate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using React.Site.Identity;
using React.Site.Models;

namespace React.Site.GraphQL
{
    public class Mutation
    {
        public async Task<TokenResponse> Login([Service] UserManager<ApplicationUser> userManager, [Service] IOptions<JwtBearerTokenSettings> jwtOptions, string email, string password)
        {
            var options = jwtOptions.Value;
            var identityUser = await ValidateUser(userManager, email, password);
            if (identityUser == null) return null;
            var token = GenerateToken(identityUser, options);
            return new TokenResponse
            {
                Token = token,
                Message = "success"
            };
        }

        private async Task<ApplicationUser> ValidateUser(UserManager<ApplicationUser> userManager, string email, string password)
        {
            var identityUser = await userManager.FindByEmailAsync(email);
            if (identityUser == null) return null;
            var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, password);
            return result == PasswordVerificationResult.Failed ? null : identityUser;
        }

        private string GenerateToken(IdentityUser identityUser, JwtBearerTokenSettings jwtBearerTokenSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, identityUser.UserName), new Claim(ClaimTypes.Email, identityUser.Email) }),
                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
