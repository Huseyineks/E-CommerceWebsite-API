using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model.DTOs
{
    public class ProductDTO
    {

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public IFormFile? ProductImage {  get; set; }

        public string? ProductPrice { get; set; }
    }
}
