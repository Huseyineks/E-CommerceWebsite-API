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
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IBaseRepository<Product> baseRepository,IProductRepository productRepository) : base(baseRepository) {
        
            _productRepository = productRepository;
        
        }

        public Product GetProductSizes(Expression<Func<Product, bool>> filter)
        {
            return _productRepository.GetProductSizes(filter);
        }
    }
}
