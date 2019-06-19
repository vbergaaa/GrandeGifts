using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Services
{
    public interface IDataService<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Read(int id);
        T Read(string key);
        IQueryable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> predicate);

    }
}
