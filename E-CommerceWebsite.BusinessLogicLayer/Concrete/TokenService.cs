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
using System.Security.Cryptography;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace E_CommerceWebsite.BusinessLogicLayer.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private ILogger<ITokenService> _logger;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager, IUserService userService, ILogger<ITokenService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;

            _userService = userService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            
        }
        public string GenerateTokenString(AppUser user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            SigningCredentials signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
         
            var securityToken = new JwtSecurityToken(

                
                expires: DateTime.Now.AddSeconds(60),
                claims : claims,
                issuer : _configuration.GetSection("Jwt:Issuer").Value,
                audience : _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials : signInCredentials

                


                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];

            using(var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);


        }

        public ClaimsPrincipal GetTokenPrincipal()
        {

            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var validation = new TokenValidationParameters
            {
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = securityKey
               
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation,out _);
        }

        public async Task<LoginResponse> RefreshToken()
        {

           
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);
            var principal = GetTokenPrincipal();


            



            var result = new LoginResponse();

            if (principal?.Identity?.Name is null)
            {

                return result;

            }
           

                var user = await _userManager.FindByNameAsync(principal.Identity.Name);

           
           
            
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
            {

                return result;

            }

            result.IsLoggedIn = true;
            result.JwtToken = this.GenerateTokenString(user);
            result.RefreshToken = this.GenerateRefreshTokenString();

            user.RefreshToken = result.RefreshToken;

            try
            {
                await _userManager.UpdateAsync(user);

            }

            catch (Exception ex)
            {
                _logger?.LogError("Token validation failed: {Message}", ex.Message);
                throw new SecurityTokenException("Invalid token format", ex);
            }

            

            return result;


        }

        public TokenStatus IsExpired()
        {
            
            
            var tokenHandler = new JwtSecurityTokenHandler();                                                        

            

            try
            {
                _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("token", out var token);

                var jwtToken = tokenHandler.ReadJwtToken(token);
                

                if (jwtToken.ValidTo <= DateTime.UtcNow)
                {
                    return TokenStatus.Expired;
                }
                else
                {
                    return TokenStatus.Valid;
                }
            }
            catch
            {
                return TokenStatus.RequiresReLogin;
            }


            
        }

        public void SetTokensInsideCookie(TokenDTO tokenDTO)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("token", tokenDTO.Token,

                new CookieOptions
                {
                    
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    
                }

                );


            _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", tokenDTO.RefreshToken,

                new CookieOptions
                {
                    
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                   
                }

                );


        }

        public bool RemoveCookies()
        {
            try
            {


                var expiredCookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Path = "/"
                };

                _httpContextAccessor.HttpContext.Response.Cookies.Append("token", "", expiredCookieOptions);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", "", expiredCookieOptions);

                return true;
            }
            catch{

                return false;
            
            }

            
            
        }
    }
}
