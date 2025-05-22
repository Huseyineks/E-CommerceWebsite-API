using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class MasterOrder
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }




        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //relation
        
        public virtual AppUser? User { get; set; }

        public int userId { get; set; }

        [JsonIgnore]
        public virtual DeliveryAdress? DeliveryAdress { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
