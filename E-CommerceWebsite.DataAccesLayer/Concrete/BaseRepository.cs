using E_CommerceWebsite.DataAccesLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.DataAccesLayer.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext db) {
        
        
            _db = db;
            _dbSet = _db.Set<T>();
        
        
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).FirstOrDefault();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

    
        public void Add(T entity)
        {

            _dbSet.Add(entity);

        }
        public void Save()
        {

            _db.SaveChanges();
        }
    }
}
