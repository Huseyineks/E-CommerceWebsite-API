using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Abstract
{
    public interface ITokenService
    {
        string GenerateTokenString(AppUser user);

        string GenerateRefreshTokenString();

        ClaimsPrincipal GetTokenPrincipal();

        Task<LoginResponse> RefreshToken();

        TokenStatus IsExpired();

        void SetTokensInsideCookie(TokenDTO tokenDTO);



        bool RemoveCookies();
       
    }
}
