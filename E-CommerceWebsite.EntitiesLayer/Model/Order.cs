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

        public string ProductName { get; set; }

        public string ProductPrice { get; set; }

        public string ProductImage { get; set; }

        public string ProductDescription { get; set; }

        public int ProductNumber { get; set; }


        public DateTime? CreatedDate { get; set; }

        //public Size? Size { get; set; }

        //public ShippingStatus? ShippingStatus { get; set; }

        public OrderStatus? OrderStatus { get; set; }





        //relations

        public virtual AppUser? User { get; set; }

        public int userId { get; set; }

        
    }
}
