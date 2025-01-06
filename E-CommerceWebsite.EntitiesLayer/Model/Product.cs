using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class Product
    {
        public int Id { get; set; }

        public Guid rowGuid { get; set; }

        public string productName { get; set; }

        public string productPrice { get; set; }

        public string productDescription { get; set; }

        public Size[]? Size { get; set; }


    }
}
