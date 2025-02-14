using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.EntitiesLayer.Model.DTOs
{
    public class OrderDTO
    {
        
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
