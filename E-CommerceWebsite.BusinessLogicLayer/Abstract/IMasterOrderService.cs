using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Abstract
{
    public interface IMasterOrderService : IBaseService<MasterOrder>
    {
        public List<MasterOrder> MOIncludeRelationTables(Expression<Func<MasterOrder, bool>> filter);
    }
}
