using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceWebsite.BusinessLogicLayer.Abstract
{
    public interface IBaseService<T> where T : class
    {
        List<T> GetAll();

        T Get(Expression<Func<T, bool>> filter);

        List<T> GetFilteredList(Expression<Func<T, bool>> filter);
        void Remove(T entity);

        void Update(T entity);

        void Add(T entity);

        void Save();
    }
}
