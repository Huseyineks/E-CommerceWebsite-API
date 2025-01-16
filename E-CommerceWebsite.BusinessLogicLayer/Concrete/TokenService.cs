using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization.Infrastructure;
using E_CommerceWebsite.EntitiesLayer.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace E_CommerceWebsite.BusinessLogicLayer.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateTokenString(AppUser user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.RowGuid.ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            SigningCredentials signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
         
            var securityToken = new JwtSecurityToken(

                
                expires: DateTime.Now.AddMinutes(60),
                claims : claims,
                issuer : _configuration.GetSection("Jwt:Issuer").Value,
                audience : _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials : signInCredentials

                


                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
