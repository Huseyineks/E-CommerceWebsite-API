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


            return Ok(_userService.GetAll());

        }
    }
}
