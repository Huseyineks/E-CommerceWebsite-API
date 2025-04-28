using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IValidator<UserDTO> _validator;
        public UserController(IUserService userService, IValidator<UserDTO> validator)
        {

            _userService = userService;
            _validator = validator;

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
    }
}
