using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class DeliveryAdress
    {
        public int Id { get; set; }
        public string Adress { get; set; }



        //relation

        public virtual AppUser User { get; set; }

        public int userId { get; set; }
        public virtual Order Order { get; set; }
    
        public int orderId { get; set; }
    }
}
