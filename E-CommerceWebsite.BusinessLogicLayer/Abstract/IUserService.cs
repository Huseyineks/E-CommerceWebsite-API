using E_CommerceWebsite.EntitiesLayer.Model;
using E_CommerceWebsite.EntitiesLayer.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Abstract
{
    public interface IUserService
    {
        List<AppUser> GetAll();

        AppUser Get(Expression<Func<AppUser, bool>> filter);

        Task<AppUser> GetHostUser();

        Task UpdateUser(AppUser user);

        
    }
}
