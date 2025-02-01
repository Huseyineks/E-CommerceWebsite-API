using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
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

        ClaimsPrincipal GetTokenPrincipal(string token);

        Task<LoginResponse> RefreshToken(RefreshTokenDTO model);

        TokenStatus IsExpired(string token);
    }
}
