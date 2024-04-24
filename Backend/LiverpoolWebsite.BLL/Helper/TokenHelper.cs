using LiverpoolWebsite.BLL.Interfaces;
using LiverpoolWebsite.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// operatiuni legate de token
// se foloseste JWT si interactioneaza cu ASP.NET Core
namespace LiverpoolWebsite.BLL.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        // constructor care init user manager si config
        public TokenHelper(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        // creez user token pentru utilizator
        public async Task<string> CreateAccessToken(User _User)
        {   
            // preiau ID-urile
            var userId = _User.Id .ToString();
            var userName = _User.UserName;

            // creez lista de claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            };

            // verific rol-ul utilizatorului (user / admin)
            var roles = await _userManager.GetRolesAsync(_User);

            foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

            // preiau cheia secreta si cheia simetrica
            var secret = _configuration.GetSection("Jwt").GetSection("Token").Get<String>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            // creez token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        // refresh token
        public string CreateRefreshToken()
        {
            // numar random pentru refresh token
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        // preia principalul dintr-un token care a expirat
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string _Token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789012345689999")),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            IdentityModelEventSource.ShowPII = true;
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            // valideaza token si returneaza principal
            var principal = tokenHandler.ValidateToken(_Token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals("hs512", StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
