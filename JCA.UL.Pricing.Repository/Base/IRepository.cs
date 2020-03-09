using System;
using System.Linq;
using System.Linq.Expressions;

namespace JCA.UL.Pricing.Repository.Base
{
    public interface IRepository<T>
    {
        T Save(T entidade);
        void Delete(T entidade);
        T GetById(int id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}