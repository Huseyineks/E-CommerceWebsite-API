using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model.DTOs
{
    public class UpdateUserDTO
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? NewPassword { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }

        public string? Neighbourhood { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? Adress { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
