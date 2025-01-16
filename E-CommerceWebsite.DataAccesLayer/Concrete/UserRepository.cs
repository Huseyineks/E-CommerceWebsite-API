using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace E_CommerceWebsite.DataAccesLayer.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        public UserRepository(UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public AppUser Get(Expression<Func<AppUser, bool>> filter)
        {
            return _userManager.Users.Where(filter).FirstOrDefault();
        }

        public List<AppUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<AppUser> GetHostUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return await _userManager.GetUserAsync(user);
        }

       
    }
}
