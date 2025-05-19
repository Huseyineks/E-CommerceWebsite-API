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
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IBaseRepository<Order> baseRepository,IOrderRepository orderRepository) : base(baseRepository) {
        
        
        
            _orderRepository = orderRepository;
        }





        public Order GetOrderWithDeliveryAdress(Expression<Func<Order, bool>> filter)
        {

            return _orderRepository.GetOrderWithDeliveryAdress(filter);

        }
    }
}
