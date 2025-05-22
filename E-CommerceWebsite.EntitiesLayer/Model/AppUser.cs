using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class AppUser : IdentityUser<int>
    {
        public Guid RowGuid { get; set; }
        public string Neighbourhood {  get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Adress {  get; set; }

        public string Role { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime RefreshTokenExpiry { get; set; }

        //relations


        [JsonIgnore]
        public virtual List<MasterOrder>? MasterOrders { get; set; }

        
        
    }
}
