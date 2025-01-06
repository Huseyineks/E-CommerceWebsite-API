using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class Order
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string productName { get; set; }

        public string productPrice { get; set; }

        public Size Size { get; set; }

        public ShippingStatus ShippingStatus { get; set; }
    }
}
