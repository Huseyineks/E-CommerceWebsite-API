using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Data;
using E_CommerceWebsite.EntitiesLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DataAccesLayer.Concrete
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {

        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db) {
        
        
            _db = db;
        
        
        }




        public Order GetOrderWithDeliveryAdress(Expression<Func<Order, bool>> filter)
        {
            return _db.Orders.Where(filter).Include(i => i.DeliveryAdress).FirstOrDefault();
        }
    }
}
