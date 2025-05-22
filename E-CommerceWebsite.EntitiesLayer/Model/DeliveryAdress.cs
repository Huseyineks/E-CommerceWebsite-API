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

    

        public virtual MasterOrder? MasterOrder { get; set; }

        public int? masterOrderId { get; set; }
    }
}
