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
    public class MasterOrderRepository : BaseRepository<MasterOrder>,IMasterOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public MasterOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<MasterOrder> MOIncludeRelationTables(Expression<Func<MasterOrder, bool>> filter)
        {
            return _db.MasterOrders.Include(i => i.Orders).Include(i => i.DeliveryAdress).Where(filter).Include(i => i.User).ToList();
        }

        
    }
}
