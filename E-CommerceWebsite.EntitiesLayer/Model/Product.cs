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

        public Guid RowGuid { get; set; }

        public string ProductName { get; set; }

        public string ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        public string ProductImage { get; set; }

        public string ProdutNumber { get; set; }

        public string? ProductType { get; set; }

        public Size[]? Size { get; set; }


    }
}
