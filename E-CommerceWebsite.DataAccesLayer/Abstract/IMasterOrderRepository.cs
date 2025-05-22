using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DataAccesLayer.Abstract
{
    public interface IMasterOrderRepository : IBaseRepository<MasterOrder>
    {
        List<MasterOrder> MOIncludeRelationTables(Expression<Func<MasterOrder, bool>> filter);
    }
}
