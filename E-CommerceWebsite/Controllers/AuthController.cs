using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IValidator<UserDTO> _validator;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser>  _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(IValidator<UserDTO> validator, IUserService userService, UserManager<AppUser> userManager,ITokenService tokenService)
        {


            _validator = validator;
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register(UserDTO userDTO) {


            var check = _validator.Validate(userDTO);

            if (!check.IsValid)
            {
                return BadRequest(new
                {
                    errors = check.Errors.Select(e => e.ErrorMessage)
                });
            }
            else
            {
                AppUser newUser = new AppUser()
                {
                    Adress = userDTO.Adress,
                    City = userDTO.City,
                    Email = userDTO.Email,
                    EmailConfirmed = true,
                    Neighbourhood = userDTO.Neighbourhood,
                    PhoneNumber = userDTO.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    PostalCode = userDTO.PostalCode,
                    RowGuid = Guid.NewGuid(),
                    Street = userDTO.Street,
                    UserName = userDTO.Username,
                    Role = "Admin"

                };
                var result = await _userManager.CreateAsync(newUser, userDTO.Password);

                if (result.Succeeded)
                {

                    return Ok(new
                    {
                        success = "KARDEŞİM SEN KENDİNİ AŞMIŞSIN HEMEN DATRABASE İNE BAK"
                    });
                }
                else
                {

                    return BadRequest(new
                    {
                        errors = result.Errors.Select(e => e.Description)
                    });
                }



               

            }







        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            
                var user = await _userManager.FindByNameAsync(userDTO.Username);
            
           

            if(user != null)
            {
                var isPasswordTrue = await _userManager.CheckPasswordAsync(user,userDTO.Password);

                if(isPasswordTrue)
                {
                    var tokenString = _tokenService.GenerateTokenString(user);
                    var refreshToken = _tokenService.GenerateRefreshTokenString();

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiry = DateTime.Now.AddHours(12);

                    await _userService.UpdateUser(user);


                    var tokenDTO = new TokenDTO
                    {
                        Token = tokenString,
                        RefreshToken = refreshToken,
                    };

                    _tokenService.SetTokensInsideCookie(tokenDTO);
                   

                    
                    return Ok();
                }
                else
                {
                    return BadRequest(new
                    {
                        errors = "Kullanıcı adı ya da parola yanlış. Lütfen tekrar deneyin."
                    }); 
                }
            }
            else
            {
                return BadRequest(new
                {
                    errors = "Kullanıcı bulunamadı. Lütfen tekrar deneyin."
                    
            });
            }
           

           

            
        }

        [HttpGet]
        [Route("api/logout")]

        public IActionResult Logout()
        {
            if (_tokenService.RemoveCookies())
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

            
        }
        [HttpGet]
        [Route("api/tokenExpired")]
        public IActionResult TokenExpired() {


            var tokenStatus = _tokenService.IsExpired();

            switch (tokenStatus)
            {
                case TokenStatus.Expired:
                    return BadRequest(new { message = "Token is expired." });
                case TokenStatus.Valid:
                    return Ok(new { message = "Token is valid." });
                case TokenStatus.RequiresReLogin:
                default:
                    return BadRequest(new { message = "Relogin is required." });
            }
           


        }
        [HttpGet]
        [Route("api/refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {

            var result =  await _tokenService.RefreshToken();

            if (result.IsLoggedIn) 
            {
                var tokenDTO = new TokenDTO()
                {
                    RefreshToken = result.RefreshToken,
                    Token = result.JwtToken
                };
                _tokenService.SetTokensInsideCookie(tokenDTO);

                return Ok();
                
            }
            else
            {
                return BadRequest(new
                {
                    response = result
                });
            }


        }


        [HttpGet]
        [Route("api/isAdmin")]

        public IActionResult IsAdmin()
        {
            var principal = _tokenService.GetTokenPrincipal();


            var role = principal.FindFirst(ClaimTypes.Role)?.Value;
            
            if (role == "Admin")
            {
                return Ok(true);
            }

            return BadRequest(false);
     
        }


    }
}
