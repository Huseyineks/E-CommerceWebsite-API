using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model
{
    public class ProductSizes
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public string Stock { get; set; }

        //relations

        [JsonIgnore]
        public virtual Product Product { get; set; }

        public int productId { get; set; }
    }
}
