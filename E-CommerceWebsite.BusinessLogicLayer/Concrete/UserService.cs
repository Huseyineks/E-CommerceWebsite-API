using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AppUser Get(Expression<Func<AppUser, bool>> filter)
        {
            return _userRepository.Get(filter);
        }

        public List<AppUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public async Task<AppUser> GetHostUser()
        {
            return await _userRepository.GetHostUser();
        }
    }
}
