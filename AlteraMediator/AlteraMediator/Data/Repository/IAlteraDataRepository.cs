using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlteraMediator.Data.Repository
{
    public interface IAlteraDataRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void CreateRange(List<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);

        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetById(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
    }
}
