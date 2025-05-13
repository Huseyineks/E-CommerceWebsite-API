using E_CommerceWebsite.EntitiesLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DataAccesLayer.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Product GetProductSizes(Expression<Func<Product, bool>> filter);
    }
}
