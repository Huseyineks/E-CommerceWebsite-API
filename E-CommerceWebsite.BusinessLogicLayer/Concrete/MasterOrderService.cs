using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Concrete
{
    public class MasterOrderService : BaseService<MasterOrder>, IMasterOrderService
    {
        private readonly IMasterOrderRepository _masterOrderRepository;
        public MasterOrderService(IBaseRepository<MasterOrder> baseRepository, IMasterOrderRepository masterOrderRepository) : base(baseRepository)
        {

            _masterOrderRepository = masterOrderRepository;
            

        }

        public List<MasterOrder> MOIncludeRelationTables(Expression<Func<MasterOrder, bool>> filter)
        {
            return _masterOrderRepository.MOIncludeRelationTables(filter);
        }
    }
}
