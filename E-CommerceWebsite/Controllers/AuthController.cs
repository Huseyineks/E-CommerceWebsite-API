using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest(new { Errors = check.Errors});
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
                    UserName = userDTO.Username

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

                    return BadRequest(result.Errors);
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
                    return Ok(new
                    {
                        token = tokenString
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        ErrorMessage = "Username or password is wrong. Please try again."
                    }); 
                }
            }
            else
            {
                return BadRequest(new
                {
                    ErrorMessage = "User is not found."
                    
            });
            }
           

           

            
        }
    }
}
