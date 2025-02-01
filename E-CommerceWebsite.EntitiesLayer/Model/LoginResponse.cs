using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

        public bool IsLoggedIn { get; set; } = false;
    }
}
