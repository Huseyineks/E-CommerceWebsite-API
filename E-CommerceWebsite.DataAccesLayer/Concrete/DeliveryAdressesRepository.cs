using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Data;
using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DataAccesLayer.Concrete
{
    public class DeliveryAdressesRepository : BaseRepository<DeliveryAdress>,IDeliveryAdressesRepository
    {

        public DeliveryAdressesRepository(ApplicationDbContext db) : base(db) { }
    }
}
