using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JCA.UL.Pricing.Repository.Base
{
    public abstract class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(T entidade)
        {
            try
            {
                _dbSet.Remove(entidade);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Save(T entidade)
        {
            try
            {
                _dbSet.Add(entidade);
                _context.SaveChanges();
                return entidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entidade)
        {
            try
            {
                _dbSet.Update(entidade);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IDbContextTransaction BeginTrans()
        {
            return _context.Database.BeginTransaction();
        }
    }
}