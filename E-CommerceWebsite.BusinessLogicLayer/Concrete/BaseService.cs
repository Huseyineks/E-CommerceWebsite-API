using E_CommerceWebsite.BusinessLogicLayer.Abstract;
using E_CommerceWebsite.DataAccesLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository) {
        
            _baseRepository = baseRepository;
        
        
        }
        public void Add(T entity)
        {
            _baseRepository.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _baseRepository.Get(filter);
        }

        public List<T> GetAll()
        {
            return _baseRepository.GetAll();
        }

        public void Remove(T entity)
        {
            _baseRepository.Remove(entity);
        }

        public void Save()
        {
           _baseRepository.Save();
        }

        public void Update(T entity)
        {
            _baseRepository.Update(entity);
        }
    }
}
