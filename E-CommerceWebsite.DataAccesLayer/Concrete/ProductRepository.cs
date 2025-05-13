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
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) {

            _db = db;
        
        }

        

        public Product GetProductSizes(Expression<Func<Product, bool>> filter)
        {
            return _db.Products.Where(filter).Include(i => i.ProductSizes).FirstOrDefault();
        }
    }
}
