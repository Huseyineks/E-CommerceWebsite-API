using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.BusinessLogicLayer.Concrete;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using E_CommerceWebsite.EntitiesLayer.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace E_CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IValidator<UserDTO> _validator;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager,IUserService userService, IValidator<UserDTO> validator, ITokenService tokenService)
        {

            _userService = userService;
            _validator = validator;
            _tokenService = tokenService;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("api/users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();

            var usersDTO = new List<UserDTO>();

            foreach(var user in users)
            {
                usersDTO.Add(new UserDTO
                {
                    Username = user.UserName,
                    Adress = user.Adress,
                    City = user.City,
                    Email = user.Email,
                    Neighbourhood = user.Neighbourhood,
                    PhoneNumber = user.PhoneNumber,
                    PostalCode = user.PostalCode,
                    Street = user.Street,
                    ConfirmPassword = "",
                    Password = ""
                });
            }

            return Ok(usersDTO);

        }



        [HttpGet]
        [Route("api/getUser")]
        public IActionResult GetUser()
        {

            var principal = _tokenService.GetTokenPrincipal();

            try
            {

                var user = _userService.Get(i => i.UserName == principal.Identity.Name);

                UserDTO userDTO = new UserDTO()
                {
                    Adress = user.Adress,
                    City = user.City,
                    Email = user.Email,
                    Neighbourhood = user.Neighbourhood,
                    PhoneNumber = user.PhoneNumber,
                    PostalCode = user.PostalCode,
                    Username = user.UserName,
                    Street = user.Street
                };
                return Ok(userDTO);
            }
            catch
            {
                return BadRequest();
            }

            




            

        }

        [HttpPut]
        [Route("api/updateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserDTO userDTO) 
        {
            var principal = _tokenService.GetTokenPrincipal();

            var user = _userService.Get(i => i.UserName == principal.Identity.Name);

            
            user.Street = user.Street != userDTO.Street && !(String.Equals(userDTO.Street,""))  ? userDTO.Street : user.Street;
            user.Adress = user.Adress != userDTO.Adress && !(String.Equals(userDTO.Adress,"")) ? userDTO.Adress : user.Adress;
            user.City = user.City != userDTO.City && !(String.Equals(userDTO.City, "")) ? userDTO.City : user.City;
            user.PostalCode = user.PostalCode != userDTO.PostalCode && !(String.Equals(userDTO.PostalCode, "")) ? userDTO.PostalCode : user.PostalCode;
            user.UserName = user.UserName != userDTO.Username && !(String.Equals(userDTO.Username, "")) ? userDTO.Username : user.UserName;

            UserDTO validateUser = new UserDTO()
            {
                Adress = user.Adress,
                City = user.City,
                Email = user.Email,
                Neighbourhood = user.Neighbourhood,
                PhoneNumber = user.PhoneNumber,
                PostalCode = user.PostalCode,
                Street = user.Street,
                Username = user.UserName,
                ConfirmPassword = "sa",
                Password = "sa"
            };

            var check = await _validator.ValidateAsync(validateUser);

            if (check.IsValid)
            {
                try
                {
                    await _userService.UpdateUser(user);

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(new
                {
                    errors = check.Errors.Select(e => e.ErrorMessage)
                });
            }

           

           

           

          
        }




        [HttpPut]
        [Route("api/updateUserPassword")]

        public async Task<IActionResult> UpdateUserPassword(UpdateUserDTO userDTO)
        {
            var principal = _tokenService.GetTokenPrincipal();

            var user = _userService.Get(i => i.UserName == principal.Identity.Name);

            if (userDTO.NewPassword != null)
            {

            
                if (userDTO.NewPassword == userDTO.ConfirmPassword)
                {
                    // Check if current password matches


                    var isPasswordValid = await _userManager.CheckPasswordAsync(user, userDTO.Password);

                    if (isPasswordValid)
                    {
                        // Generate password reset token
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        // Reset password with new password
                        var result = await _userManager.ResetPasswordAsync(user, token, userDTO.NewPassword);

                        if (!result.Succeeded)
                        {
                            return BadRequest(new { message = "Şifre güncellenirken bir hata oluştu." });
                        }
                        else
                        {
                            try
                            {
                                await _userService.UpdateUser(user);

                                return Ok();
                            }
                            catch
                            {
                                return BadRequest(new { message = "Kullanıcı bilgileri güncellenirken bir hata oluştu." });
                            }
                        }
                    }
                    else
                    {
                        return BadRequest(new { message = "Mevcut şifre yanlış." });
                    }



                }
                    else
                    {
                        return BadRequest(new { message = "Lütfen yeni şifrelerin eşit olduğundan emin ol." });
                    }

        }
        else
        {
            return BadRequest(new { message = "Lütfen yeni şifreyi girin." });
        }


            
        }


    }
}
