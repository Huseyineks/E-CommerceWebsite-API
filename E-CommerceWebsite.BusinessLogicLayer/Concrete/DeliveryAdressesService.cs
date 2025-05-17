﻿using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Concrete
{
    public class DeliveryAdressesService : BaseService<DeliveryAdress>,IDeliveryAdressesService
    {
        public DeliveryAdressesService(IBaseRepository<DeliveryAdress> baseRepository) : base(baseRepository) { }
       
    }
}
