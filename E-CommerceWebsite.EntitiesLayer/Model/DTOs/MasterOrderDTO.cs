using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model.DTOs
{
    public class MasterOrderDTO
    {
        public string Guid { get; set; }
        public List<Order> Orders { get; set;}

        public string DeliveryAdress { get; set;}

        public DateTime CreatedDate { get; set;}


    }
}
